using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRidOfTextScript : MonoBehaviour
{
    public GameObject billboard;
    public AudioSource source;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand")) {
            billboard.SetActive(false);
            source.Stop();
            gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
