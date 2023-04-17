using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCard : MonoBehaviour
{
    public Quaternion startRotation;
    public Quaternion targetRotation;
    private float smooth = 30.0f;
    
    public bool flipped;

    void Start()
    {
        startRotation = transform.rotation;
        flipped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player touch card");
            flipped = true;
            
        }
    }

    void Update()
    {
        targetRotation = transform.rotation;
        if (flipped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * smooth);
        }
    }
}
