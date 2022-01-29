using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI : MonoBehaviour
{
	public TextMeshProUGUI distanceText;
	public TextMeshProUGUI bestDistanceText;
	public Transform player;

	private float bestDistance = 0f;
	private float currentDistance = 0f;
	private float playerDistance = 0f;

    void Start()
    {
		bestDistanceText.text = "Best: " + bestDistance.ToString("0") + "m";
        distanceText.text = "Starting...";
        bestDistance = 0f;
        currentDistance = 0f;
    }

    void Update()
    {
		playerDistance = player.position.z;
		if (currentDistance > bestDistance){
			bestDistance = currentDistance;
		}
		currentDistance = playerDistance;
        distanceText.text = "Distance: " + currentDistance.ToString("0") + "m";
        bestDistanceText.text = "Best: " + bestDistance.ToString("0") + "m";
    }
}
