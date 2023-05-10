using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AsteroidController : MonoBehaviour
{
    public Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Transform thisRock;
    private float move = 0.5f;
    public bool elevator;
    public GameObject wand;
    public GameObject player;
    private Interactable wandy;
    public AudioSource source;
    public AudioClip rumble;

    void Start()
    {
        startPosition = transform.position;
        thisRock = GetComponent<Transform>();
        elevator = false;
        wandy = wand.GetComponent<Interactable>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(thisRock, true);
            elevator = true;
            Debug.Log("player on");
            source.PlayOneShot(rumble);
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
    

    void Update()
    {
        currentPosition = transform.position;
        /*if (wandy.attachedToHand)
        {
            Debug.Log("wand in hand");
            elevator = true;
        } 
        else
        {
            elevator = false;
        }
        */

        if (elevator)
        {
            player.transform.SetParent(thisRock, true);
            transform.position = Vector3.Lerp(currentPosition, endPosition, move * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(currentPosition, startPosition, move * Time.deltaTime);
            player.transform.SetParent(null);
        }
    }
}
