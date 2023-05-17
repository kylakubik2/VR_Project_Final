using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private float lifeSpan = 10.0f;
    public GameObject rayGun;

    private ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        rayGun = GameObject.Find("rayGun");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UFO"))
        {
            explosion = other.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
            explosion.Play();
            Destroy(other.gameObject);
            rayGun.GetComponent<LaserGunScript>().numHit++;
            rayGun.GetComponent<LaserGunScript>().SetShootText();
            rayGun.GetComponent<LaserGunScript>().hit = true;
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
