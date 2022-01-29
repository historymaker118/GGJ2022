using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelGeometry levelGeometry;
    public Transform player;

    public Transform camera;

    public bool isInside = true;

    public float moveSpeed = 12.0f;
    public float turnSpeed = 4.0f;

    public float playerX = 0; // technically an angle
    public float playerZ = 0;

    public float shipOffset = 0.75f;

    public int sides = 6;
    public int rings = 32;

    public float ringDepth = 8.0f;
    public float radius = 3.0f;

    public float camOffset = 10.0f;
    public float camRadiusOutside = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        levelGeometry.GenerateTubeMesh(sides, rings, ringDepth, radius);
    }

    // Update is called once per frame
    void Update()
    {
        playerX += Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

        if (Input.GetKeyDown(KeyCode.S))
        {
            isInside = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isInside = true;
        }

        playerZ = playerZ + moveSpeed * Time.deltaTime;

        var offset = radius + ((isInside) ? -1 : 1 ) * shipOffset;

        // TODO WT: Keep player offset from surface of hexagon.
        player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(Mathf.Sin(playerX) * offset, -Mathf.Cos(playerX) * offset, playerZ), Time.deltaTime * 20.0f);

        // TODO WT: Lock rotation to snap to the edge of the hexagon.
        player.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * playerX, Vector3.forward);

        Vector3 newPos;
        if (isInside)
        {
            newPos = new Vector3(0.0f, 0.0f, player.transform.position.z - camOffset);
        } else
        {
            newPos = new Vector3(Mathf.Sin(playerX) * (offset + camRadiusOutside), -Mathf.Cos(playerX) * (offset + camRadiusOutside), player.transform.position.z - camOffset);
        }

        camera.transform.position = Vector3.Lerp(camera.transform.position, newPos, Time.deltaTime * 10.0f);
        camera.transform.rotation = player.transform.rotation;
    }
}
