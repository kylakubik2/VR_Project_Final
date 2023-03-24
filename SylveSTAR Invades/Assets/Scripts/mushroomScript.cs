using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomScript : MonoBehaviour
{
    public GameObject buggy;
    private Rigidbody rb;
    public AudioSource source;
    public AudioClip mushroom;

    // Start is called before the first frame update
    void Start()
    {
        rb = buggy.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Buggy"))
        {
            gameObject.SetActive(false);
            rb.velocity = (rb.velocity.magnitude * 1.5f) * rb.velocity.normalized;
            source.PlayOneShot(mushroom);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
