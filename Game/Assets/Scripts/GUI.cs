using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI : MonoBehaviour
{
	public float milestoneGap = 125f;
	public AudioSource Synth;
	public AudioSource Kick;
	public AudioSource Hihat;
	public AudioSource Bass;

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
        
        Synth.volume = 0;
        Kick.volume = 0;
        Hihat.volume = 0;
        Bass.volume = 1f;
    }

    void Update()
    {
		playerDistance = player.position.z;
		if (currentDistance > bestDistance){
			bestDistance = currentDistance;
		}
		if (playerDistance < currentDistance){
			//We've reset
			Kick.volume = 0;
			Hihat.volume = 0;
			Synth.volume = 0;
		}
		currentDistance = playerDistance;
        distanceText.text = "Distance: " + currentDistance.ToString("0") + "m";
        bestDistanceText.text = "Best: " + bestDistance.ToString("0") + "m";
        
        if (currentDistance > milestoneGap * 1f){
			Kick.volume = 1f;
		}
		if (currentDistance > milestoneGap * 2f){
			Hihat.volume = 1f;
		}
		if (currentDistance > milestoneGap * 3f){
			Synth.volume = 1f;
		}
		if (currentDistance > milestoneGap * 6f){
			Bass.volume = 0f;
		}
		if (currentDistance > milestoneGap * 8f){
			Bass.volume = 1f;
		}
    }
}
