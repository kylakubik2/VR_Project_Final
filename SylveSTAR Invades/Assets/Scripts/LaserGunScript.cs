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
    public AudioSource win;
    public AudioClip laserShoot;
    public AudioClip explode;
    public AudioSource gameLose;

    public Transform laserOrigin;
    LineRenderer laser;

    public TextMeshPro timerText;
    public TextMeshPro shootText;
    public TextMeshPro shootToBeat;

    public float shootPower;
    public GameObject ball;

    public float laserFireTime = 0.5f;
    public float laserTimer = -10.0f;

    private float winTime = 100000.0f;

    public float gameTimer = 20.0f;
    private int milliseconds;
    private int seconds;
    private int minutes;

    public bool startTimer;
    public GameObject UFOGeneratorObject;
    public UFOGenerator ufoGenerator;
    private bool startGame = false;

    private int sylvestarScore = 28; //TODO: set sylvestarScore
    public int numHit;
    public bool hit;
    public GameObject player;
    //private Vector3 startPosition = new Vector3(4.0f, 1467.5f, -651.0f);
    public GameObject portal;
    public bool gameOver;
    public bool hasWon;

    public bool gunInHand = false;
    private Interactable interactable;

    public GameObject cylinder;

    public GameObject[] ufos;

    void Start()
    {
        win.enabled = false;
        
        hasWon = false;
        gameOver = false;
        //portal.SetActive(false);
        shootGun.AddOnStateDownListener(TriggerDown, rightHand);
        shootGun.AddOnStateUpListener(TriggerUp, rightHand);
        interactable = GetComponent<Interactable>();

        startTimer = false;
        hit = false;

        SetShootText();

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
        if (gameTimer == 20.0f)
        {
            startGame = true;
        }
        if (gameTimer > 0)
        {
            Instantiate(ball, laserOrigin.position, laserOrigin.rotation * Quaternion.Euler(0f, 0f, 0f)).GetComponent<Rigidbody>().AddForce(laserOrigin.forward * shootPower);
        }
        source.PlayOneShot(laserShoot);
    }

    public void SetShootText()
    {
        shootText.text = numHit.ToString();
    }
    
    void SetTimerText()
    {
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + milliseconds.ToString("D2");
    }

    void Update()
    {
        if (startGame)
        {
            ufoGenerator.startUFOs = true;
            UFOGeneratorObject.SetActive(true);
            startGame = false;
        }

        if (startTimer)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            { 
                ufoGenerator.stopUFOs = true;
                UFOGeneratorObject.SetActive(false);
                winTime = Time.time;
                startTimer = false;
                startGame = false;

                ufos = GameObject.FindGameObjectsWithTag("UFO");
                foreach (GameObject ufo in ufos)
                {
                    ufo.SetActive(false);
                }
            }
        }
        if (hit)
        {
            hit = false;
            source.PlayOneShot(explode, 1.0f);
        }

        if (Time.time > (winTime + 1.0f))
        {
            Debug.Log("WHOOP");
            gameOver = true;
            //player.transform.position = startPosition;
            if (gameOver)
            {
                if (numHit > sylvestarScore)
                {
                    hasWon = true;
                    portal.SetActive(true);
                    win.enabled = true;
                }
                else
                {
                    portal.SetActive(true);
                    gameLose.enabled = true;
                }
            }

            winTime = 100000.0f;
        }

        if (hasWon)
        {
            cylinder.SetActive(false);
        }

        SetShootText();

        minutes = (int)(gameTimer / 60f) % 60;
        seconds = (int)(gameTimer % 60f);
        milliseconds = (int)(gameTimer * 1000f) % 1000;

        if (startTimer)
        {
            SetTimerText();
        }
        else
        {
            timerText.text = "00:00:00";
        }
    }
}
