using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI : MonoBehaviour
{
	public TextMeshProUGUI distanceText;
	public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        distanceText.text = "Starting...";
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = "Distance: " + player.position.z.ToString("0");
    }
}
