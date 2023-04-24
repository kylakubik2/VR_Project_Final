using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Vector3 endPosition;
    public Vector3 startPosition;
    public Vector3 currentPosition;
    private Transform thisRock;
    private float move = 0.5f;
    public bool elevator;
    public bool stay;

    void Start()
    {
        startPosition = transform.position;
        thisRock = GetComponent<Transform>();
        elevator = false;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(thisRock, true);
            elevator = true;
            Debug.Log("player on");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
            elevator = false;
            Debug.Log("player off");
        }
    }
    */

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player colliding");
            stay = true;
        } else
        {
            Debug.Log("player not colliding");
            stay = false;
        }
    }

    void Update()
    {
        currentPosition = transform.position;
        if (stay)
        {
            elevator = true;
        } 
        else
        {
            elevator = false;
        }

        if (elevator)
        {
            transform.position = Vector3.Lerp(currentPosition, endPosition, move * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(currentPosition, startPosition, move * Time.deltaTime);
        }
    }
}
