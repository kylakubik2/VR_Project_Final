using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuggyController : MonoBehaviour
{
    private Rigidbody rb;

    public TextMeshProUGUI lapCountText;
    public TextMeshProUGUI timerText;
    private float timeNum;
    private float startEffectTime;
    private Vector3 initVelocity;
    private int milliseconds;
    private int seconds;
    private int minutes;
    private bool timing;
    private int numLaps;

    public float Maxspeed = 20f;
    public float CurrentSpeed = 0.0f;
    private bool speedy = false;

    public AudioSource source;
    public AudioClip boostClip;
    public AudioClip lastLap;
    public AudioClip cheer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        numLaps = 0;
        timeNum = 0.0f;
        timing = false;

        SetLapText();
    }

    void SetLapText()
    {
        lapCountText.text = "Lap " + numLaps.ToString() + "/3";
    }
    void SetTimerText()
    {
        timerText.text = "Time: " + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + milliseconds.ToString("D2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mushroom"))
        {
            other.gameObject.SetActive(false);
            startEffectTime = Time.time;
            source.PlayOneShot(boostClip);
            initVelocity = rb.velocity;
            speedy = true;
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            timing = true;

            if (numLaps < 3)
            {
                numLaps = numLaps + 1;
                if (numLaps == 3)
                {
                    source.PlayOneShot(lastLap);
                }
            }
            else if (numLaps == 3)
            {
                source.PlayOneShot(cheer);
                timing = false;
            }

            SetLapText();
        }
    }

    void FixedUpdate()
    {
        CurrentSpeed = rb.velocity.magnitude;

        if (timing)
        {
            timeNum += Time.deltaTime;
        }

        if (Time.time >= (startEffectTime + 5.0f))
        {
            rb.velocity = initVelocity;
            speedy = false;
        }

        if (speedy && CurrentSpeed < Maxspeed)
        {
            CurrentSpeed *= 1.25f;
            rb.velocity = Vector3.forward * CurrentSpeed;
        }

        minutes = (int)(timeNum / 60f) % 60;
        seconds = (int)(timeNum % 60f);
        milliseconds = (int)(timeNum * 1000f) % 1000;

        SetTimerText();
    }
}
