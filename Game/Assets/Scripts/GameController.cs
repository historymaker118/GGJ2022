using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelGeometry levelGeometry;
    public Transform player;

    new public Transform camera;

    public ColorSwapPostProcess colorSwap;

    public bool isInside = true;

    public float moveSpeed = 10.0f;
    public float acceleration = 0.1f;
    public float minSpeed = 10.0f;
    public float maxSpeed = 20.0f;
    public float turnSpeed = 4.0f;

    public float playerX = 0; // technically an angle
    public float playerZ = 0;

    public float shipOffset = 0.75f;

    public int sides = 6;
    public int rings = 32;

    public float ringDepth = 8.0f;
    public float radius = 3.0f;

    public float camXOffset = 10.0f;
    public float camYOffset = 0.0f;
    public float camYOffsetOutside = 0.0f;

    Coroutine tween;

    float currentCrossfade = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
		moveSpeed = minSpeed;
        levelGeometry.Generate(sides, rings, ringDepth, radius);
    }

    IEnumerator TweenForCamera(float from, float to)
    {
        var tweenTime = 0.3f;
        var startTime = Time.time;
        var endTime = startTime + tweenTime;

        while (Time.time < endTime)
        {
            yield return new WaitForEndOfFrame();

            currentCrossfade = Mathf.Clamp01((endTime - Time.time) / tweenTime);

            colorSwap.setCrossfade(Mathf.Lerp(from, to, 1.0f - currentCrossfade));
        }

        tween = null;
    }

    public void HandlePlayerHitSomething()
    {
        Debug.Log("Player Hit Something, Restarting");
        moveSpeed = minSpeed;
        playerZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerX += Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

        if (Input.GetKeyDown(KeyCode.S))
        {
            isInside = false;
            if (tween != null)
            {
                StopCoroutine(tween);
            }

            StartCoroutine(TweenForCamera(0.0f, 1.0f));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isInside = true;
            if (tween != null)
            {
                StopCoroutine(tween);
            }
            StartCoroutine(TweenForCamera(1.0f, 0.0f));
        }

        playerZ = playerZ + moveSpeed * Time.deltaTime;
        moveSpeed += acceleration * Time.deltaTime;
        if (moveSpeed > maxSpeed){
			moveSpeed = maxSpeed;
		}

        var offsetDirection = (isInside) ? -1 : 1;

        var offset = radius + (offsetDirection * shipOffset);

        // TODO WT: Keep player offset from surface of hexagon.
        player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(Mathf.Sin(playerX) * offset, -Mathf.Cos(playerX) * offset, playerZ), Time.deltaTime * 20.0f);

        // TODO WT: Lock rotation to snap to the edge of the hexagon.
        player.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * playerX, Vector3.forward);

        Vector3 newPos;
        // !Not really too bothered about having the camera centererd when inside, this way we'll get some nice camera movement
        if (isInside)
        {
            newPos = new Vector3(Mathf.Sin(playerX) * (offset + camYOffset * offsetDirection), -Mathf.Cos(playerX) * (offset + camYOffset * offsetDirection), player.transform.position.z - camXOffset);

        }
        else
        {
            newPos = new Vector3(Mathf.Sin(playerX) * (offset + camYOffsetOutside * offsetDirection), -Mathf.Cos(playerX) * (offset + camYOffsetOutside * offsetDirection), player.transform.position.z - camXOffset);
        }

        // if moving left, offset camera left a bit more, vice versa.

        // TODO WT: Camera shake?

        camera.transform.position = Vector3.Lerp(camera.transform.position, newPos, Time.deltaTime * 10.0f);
        camera.transform.rotation = player.transform.rotation;
    }
}
