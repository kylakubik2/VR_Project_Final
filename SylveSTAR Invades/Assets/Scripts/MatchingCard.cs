using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCard : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player touch card");
        }
    }

    void Update()
    {
        
    }
}
