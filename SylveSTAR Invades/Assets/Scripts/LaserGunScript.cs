using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using TMPro;

[RequireComponent(typeof(Throwable))]
public class LaserGunScript : MonoBehaviour
{
    public SteamVR_Action_Boolean shootGun;
    public SteamVR_Input_Sources rightHand;

    public AudioSource source;
    public AudioClip carnivalMusic;
    public AudioClip laserShoot;
    public AudioClip explode;

    public Transform laserOrigin;
    LineRenderer laser;

    public TextMeshPro shootText;
    public TextMeshPro shootToBeat;

    public float shootPower;
    public GameObject ball;

    public float laserFireTime = 0.5f;
    public float laserTimer = -10.0f;

    public float gameTimer = 60.0f;

    public bool startTimer;
    public GameObject ufoGenerator;

    public int numHit;

    void Start()
    {
        shootGun.AddOnStateDownListener(TriggerDown, rightHand);
        shootGun.AddOnStateUpListener(TriggerUp, rightHand);
        laser = GetComponent<LineRenderer>();

        startTimer = false;

        SetShootText();
        SetShootToBeatText();

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
        if (gameTimer > 0)
        {
            Instantiate(ball, laserOrigin.position, laserOrigin.rotation * Quaternion.Euler(0f, 0f, 0f)).GetComponent<Rigidbody>().AddForce(laserOrigin.forward * shootPower);
        }
        //source.PlayOneShot(laserShoot);
    }

    public void SetShootText()
    {
        shootText.text = "Number Hit: " + numHit.ToString();
    }
    void SetShootToBeatText()
    {
        shootText.text = "Sylvestar's Score: ";
    }

    public void PlayExplodeSound()
    {
        source.PlayOneShot(explode);
    }

    void Update()
    {
        if (startTimer)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            {
                ufoGenerator.SetActive(false);
            }
        }
    }
}
