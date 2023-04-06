using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private float lifeSpan = 10.0f;
    public LaserGunScript laserGunScript;
    public AudioSource source;
    public AudioClip explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UFO"))
        {
            Destroy(other.gameObject);
            laserGunScript.numHit++;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpan > 0)
        {
            lifeSpan -= Time.deltaTime;
        } 
        else
        {
            Destroy(this.gameObject);
        }
    }
}
