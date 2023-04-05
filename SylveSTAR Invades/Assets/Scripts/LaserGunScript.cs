using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class LaserGunScript : MonoBehaviour
{
    public SteamVR_Action_Boolean shootGun;
    public SteamVR_Input_Sources rightHand;

    public AudioSource source;
    public AudioClip carnivalMusic;
    public AudioClip laserShoot;
    public AudioClip explodeSound;

    public Transform laserOrigin;
    LineRenderer laser;

    public float laserFireTime = 0.5f;
    public float laserTimer = -10.0f;

    public float gameTimer = 60.0f;

    public bool startTimer;
    public bool stopUFO;

    public int numHit;

    void Start()
    {
        shootGun.AddOnStateDownListener(TriggerDown, rightHand);
        shootGun.AddOnStateUpListener(TriggerUp, rightHand);
        laser = GetComponent<LineRenderer>();

        startTimer = false;
        stopUFO = false;

        numHit = 0;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Up");
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Down");
        startTimer = true;
        laserTimer = laserFireTime;
        Shoot(laserOrigin.position, Vector3.forward);
        laser.enabled = true;
        source.PlayOneShot(laserShoot);
    }

    void Shoot(Vector3 startPosition, Vector3 direction)
    {
        Ray ray = new Ray(startPosition, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000) && !stopUFO)
        {
            if (hit.collider.gameObject.tag == "UFO")
            {
                //var exp = hit.GetComponent<ParticleSystem>();
                //exp.Play();
                Destroy(hit.collider.gameObject);
                source.PlayOneShot(explodeSound);
                numHit++;
            }
        }
    }

    void Update()
    {
        if (laserTimer > 0)
        {
            laserTimer -= Time.deltaTime;
            Shoot(laserOrigin.position, Vector3.forward);
        }
        else
        {
            laser.enabled = false;
        }

        if (startTimer)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            {
                stopUFO = true;
            }
        }
    }
}
