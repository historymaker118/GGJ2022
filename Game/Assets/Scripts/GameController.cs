using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TweenParams
{
    public float colorCrossfade;
    public float playerY;
    public float cameraY;
    public float cameraZ;
    public float cameraPitch;
    public bool isInside;
}

public class GameController : MonoBehaviour
{
    public LevelGeometry levelGeometry;
    public Transform player;

    new public Transform camera;

    public ColorSwapPostProcess colorSwap;

    public float moveSpeed = 10.0f;
    public float acceleration = 0.1f;
    public float minSpeed = 10.0f;
    public float maxSpeed = 20.0f;
    public float turnSpeed = 4.0f;

    public float playerX = 0; // technically an angle
    public float playerZ = 0;

    public TweenParams currentParams;

    public TweenParams insideTweenTarget = new TweenParams
    {
        colorCrossfade = 0.0f,
        playerY = -1.0f,
        cameraY = 1.5f,
        cameraZ = -2.0f,
        cameraPitch = 0.0f,
        isInside = true,
    };

    public TweenParams outsideTweenTarget = new TweenParams
    {
        colorCrossfade = 1.0f,
        playerY = 0.5f,
        cameraY = 1.9f,
        cameraZ = -2.0f,
        cameraPitch = -30.0f,
        isInside = false,
    };

    public int sides = 6;
    public int rings = 32;

    public float ringDepth = 8.0f;
    public float radius = 3.0f;

    Coroutine tween;

    // Start is called before the first frame update
    void Start()
    {
		moveSpeed = minSpeed;
        levelGeometry.Generate(sides, rings, ringDepth, radius);

        currentParams = insideTweenTarget;
        colorSwap.setCrossfade(insideTweenTarget.colorCrossfade);
    }

    IEnumerator TweenToOutside(float duration, TweenParams tweenParams)
    {
        var startTime = Time.time;
        var endTime = startTime + duration;
        
        var startParams = currentParams;
        
        while (Time.time < endTime)
        {
            yield return new WaitForEndOfFrame();
            var alpha = 1.0f - Mathf.Clamp01((endTime - Time.time) / duration);

            currentParams = new TweenParams
            {
                colorCrossfade = Mathf.Lerp(startParams.colorCrossfade, tweenParams.colorCrossfade, alpha),
                playerY =        Mathf.Lerp(startParams.playerY,        tweenParams.playerY, alpha),
                cameraY =        Mathf.Lerp(startParams.cameraY,        tweenParams.cameraY, alpha),
                cameraZ =        Mathf.Lerp(startParams.cameraZ,        tweenParams.cameraZ, alpha),
                cameraPitch =    Mathf.Lerp(startParams.cameraPitch,    tweenParams.cameraPitch, alpha),
            };

            colorSwap.setCrossfade(currentParams.colorCrossfade);
        }

        currentParams = tweenParams;
        colorSwap.setCrossfade(tweenParams.colorCrossfade);

        tween = null;
    }

    public void HandlePlayerHitSomething()
    {
        Debug.Log("Player Hit Something, Restarting");
        moveSpeed = minSpeed;

        if (tween != null)
        {
            StopCoroutine(tween);
        }

        playerZ = 0.0f;
        currentParams = insideTweenTarget;

        colorSwap.setCrossfade(insideTweenTarget.colorCrossfade);
    }

    // Update is called once per frame
    void Update()
    {
        playerX += Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;


        if (Input.GetKeyDown(KeyCode.S) && currentParams.isInside)
        {
            if (tween != null)
            {
                StopCoroutine(tween);
            }

            tween = StartCoroutine(TweenToOutside(0.3f, outsideTweenTarget));
        }

        if (Input.GetKeyDown(KeyCode.W) && !currentParams.isInside)
        {
            if (tween != null)
            {
                StopCoroutine(tween);
            }

            tween = StartCoroutine(TweenToOutside(0.3f, insideTweenTarget));
        }

        playerZ = playerZ + moveSpeed * Time.deltaTime;
        moveSpeed += acceleration * Time.deltaTime;
        if (moveSpeed > maxSpeed){
			moveSpeed = maxSpeed;
		}

        var playerY = radius + currentParams.playerY;

        player.transform.position = new Vector3(Mathf.Sin(playerX) * playerY, -Mathf.Cos(playerX) * playerY, playerZ);
        player.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * playerX, Vector3.forward);

        camera.transform.localPosition = new Vector3(0.0f, currentParams.cameraY, currentParams.cameraZ);

        camera.transform.localRotation = Quaternion.AngleAxis(currentParams.cameraPitch, Vector3.right);
    }
}
