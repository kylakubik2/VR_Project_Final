using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class finishLineScript : MonoBehaviour
{
    public TextMeshPro lapText;
    public TextMeshPro timerText;
    public TextMeshPro timeToBeat;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 buggyStart;

    public GameObject buggy;
    public AudioSource source;
    public AudioClip lastLap;
    public AudioClip cheer;
    public GameObject player;

    private float timeNum;
    private int milliseconds;
    private int seconds;
    private int minutes;
    private bool timing;
    private int numLaps;

    private GameObject[] mushrooms;

    private float winTime = 100000.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeNum = 0.0f;
        timing = false;
        numLaps = 0;

        SetLapText();
        SetTimerText();

        buggyStart = buggy.transform.position;

        mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");
    }
    void SetLapText()
    {
        lapText.text = "Lap " + numLaps.ToString() + "/3";
    }
    void SetTimerText()
    {
        timerText.text = "Time: " + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + milliseconds.ToString("D2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Buggy"))
        {
            timing = true;
            if (numLaps < 3)
            {
                numLaps = numLaps + 1;
                if (numLaps == 3)
                {
                    source.PlayOneShot(lastLap);
                }
                foreach(GameObject shroom in mushrooms)
                {
                    shroom.gameObject.SetActive(true);
                }
            }
            else if (numLaps == 3)
            {
                winTime = Time.time;
                source.PlayOneShot(cheer);
            }

            SetLapText();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timing)
        {
            timeNum += Time.deltaTime;
        }

        minutes = (int)(timeNum / 60f) % 60;
        seconds = (int)(timeNum % 60f);
        milliseconds = (int)(timeNum * 1000f) % 1000;

        if (Time.time > (winTime + 5.0f))
        {
            player.transform.position = startPosition;
            lapText.enabled = false;
            timerText.enabled = false;
            timeToBeat.enabled = false;

            source.enabled = false;
            buggy.transform.position = buggyStart;

            winTime = 100000.0f;
        }

        if (winTime > 50000.0f)
        {
            SetTimerText();
        }
    }
}
