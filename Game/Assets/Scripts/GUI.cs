using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI : MonoBehaviour
{
	public float milestoneGap = 100f;
	public int milestoneCooldown = 3;
	public AudioSource Synth;
	public AudioSource Kick;
	public AudioSource Hihat;
	public AudioSource Bass;
	public AudioSource Milestone;

	public TextMeshProUGUI distanceText;
	public TextMeshProUGUI bestDistanceText;
	public TextMeshProUGUI milestoneText;
	public Transform player;

	private float bestDistance = 0f;
	private float currentDistance = 0f;
	private float playerDistance = 0f;

	private bool milestoneHit_0 = false;
	private bool milestoneHit_1 = false;
	private bool milestoneHit_2 = false;
	private bool milestoneHit_3 = false;
	private bool milestoneHit_4 = false;
	private bool milestoneHit_5 = false;
	private bool milestoneHit_6 = false;
	private bool milestoneHit_7 = false;
	private bool milestoneHit_8 = false;
	private bool milestoneHit_9 = false;
	private bool milestoneHit_10 = false;
	private bool milestoneHit_11 = false;

    void Start()
    {
		bestDistanceText.text = "Best: " + bestDistance.ToString("0") + "m";
        distanceText.text = "Distance: 0m";
        milestoneText.text = "Get Ready!";
        bestDistance = 0f;
        currentDistance = 0f;
        
        Synth.volume = 0;
        Kick.volume = 0;
        Hihat.volume = 0;
        Bass.volume = 1f;

		milestoneHit_0 = false;
        milestoneHit_1 = false;
        milestoneHit_2 = false;
        milestoneHit_3 = false;
        milestoneHit_4 = false;
        milestoneHit_5 = false;
        milestoneHit_6 = false;
        milestoneHit_7 = false;
        milestoneHit_8 = false;
        milestoneHit_9 = false;
        milestoneHit_10 = false;
        milestoneHit_11 = false;
    }

    void Update()
    {
		playerDistance = player.position.z;
		if (currentDistance > bestDistance){
			bestDistance = currentDistance;
		}
		if (playerDistance < currentDistance){
			//We've reset
			milestoneText.text = "Get Ready!";
			Bass.volume = 1f;
			Kick.volume = 0;
			Hihat.volume = 0;
			Synth.volume = 0;
			
			milestoneHit_0 = false;
			milestoneHit_1 = false;
			milestoneHit_2 = false;
			milestoneHit_3 = false;
			milestoneHit_4 = false;
			milestoneHit_5 = false;
			milestoneHit_6 = false;
			milestoneHit_7 = false;
			milestoneHit_8 = false;
			milestoneHit_9 = false;
			milestoneHit_10 = false;
			milestoneHit_11 = false;
		}
		currentDistance = playerDistance;
        distanceText.text = "Distance: " + currentDistance.ToString("0") + "m";
        bestDistanceText.text = "Best: " + bestDistance.ToString("0") + "m";
        
        if (currentDistance < milestoneGap && !milestoneHit_0){
			StartCoroutine("ShowText");
			milestoneHit_0 = true;
		}
        if (currentDistance > milestoneGap * 1f && !milestoneHit_1){
			Kick.volume = 1f;
			milestoneText.text = "Reached 100m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_1 = true;
		}
		if (currentDistance > milestoneGap * 2f && !milestoneHit_2){
			Hihat.volume = 1f;
			milestoneText.text = "Reached 200m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_2 = true;
		}
		if (currentDistance > milestoneGap * 3f && !milestoneHit_3){
			Synth.volume = 1f;
			milestoneText.text = "Reached 300m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_3 = true;
		}
		if (currentDistance > milestoneGap * 4f && !milestoneHit_4){
			milestoneText.text = "Reached 400m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_4 = true;
		}
		if (currentDistance > milestoneGap * 5f && !milestoneHit_5){
			milestoneText.text = "Reached 500m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_5 = true;
		}
		if (currentDistance > milestoneGap * 6f && !milestoneHit_6){
			Bass.volume = 0f;
			milestoneText.text = "Reached 600m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_6 = true;
		}
		if (currentDistance > milestoneGap * 8f && !milestoneHit_7){
			Bass.volume = 1f;
			milestoneText.text = "Reached 800m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_7 = true;
		}
		if (currentDistance > milestoneGap * 10f && !milestoneHit_8){
			milestoneText.text = "Reached 1000m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_8 = true;
		}
		if (currentDistance > milestoneGap * 12f && !milestoneHit_9){
			milestoneText.text = "Reached 1200m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_9 = true;
		}
		if (currentDistance > milestoneGap * 15f && !milestoneHit_10){
			milestoneText.text = "Reached 1500m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_10 = true;
		}
		if (currentDistance > milestoneGap * 200f && !milestoneHit_11){
			milestoneText.text = "Reached 2000m";
			Milestone.Play();
			StartCoroutine("ShowText");
			milestoneHit_11 = true;
		}
    }
    
    private IEnumerator ShowText(){
		milestoneText.enabled = true;
		int counter = 0;
		while (counter <= milestoneCooldown){
			counter ++;
			yield return new WaitForSeconds(1);
		}
		milestoneText.enabled = false;
	}
}
