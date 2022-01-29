using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetection : MonoBehaviour
{
    public UnityEvent onHitSomething;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Invoking the thing");
        onHitSomething.Invoke();
    }
}
