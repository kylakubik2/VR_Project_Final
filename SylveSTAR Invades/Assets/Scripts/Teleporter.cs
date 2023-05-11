using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Teleporter : MonoBehaviour
{
    // these Vector3's are "global" positions relative to the parent scene
    private Vector3 blackRoomPosition = new Vector3(-2272.43f, 5190f, 0f);
    private Vector3 startPosition = new Vector3(21.55f, 225.8f, -769.35f);
    private Vector3 racingPosition = new Vector3(-473.71f, 658.24f, -914.75f); 
    private Vector3 golf1Position = new Vector3(219.635f, 505.296f, -1229.09f);
    private Vector3 golf2Position = new Vector3(244.818f, 512.73f, -1232.788f);
    private Vector3 golf3Position = new Vector3(255.01f, 519.74f, -1211.8f);
    private Vector3 hangmanPosition = new Vector3(0.44f, 9.76f, -1.75f); 
    private Vector3 shootingPosition = new Vector3(633.57f, 410.95f, -354.45f); 
    private Vector3 matchingPosition = new Vector3(353.19f, 939.21f, -428.32f);

    public GameObject player;

    public AudioSource mainRoomAudio;
    public AudioClip mainRoomNarrationAudio;

    public AudioSource racingAudio;
    public AudioClip racingNarrationAudio;

    public AudioSource golf1Audio;
    public AudioSource golf2Audio;
    public AudioSource golf3Audio;
    public AudioClip golfNarrationAudio;

    public GameObject ufoGenerator;
    public AudioClip shootingNarrationAudio;
    public AudioSource shootingAudio;

    public MatchingGame matchingGame;
    public AudioClip matchingNarrationAudio;
    public AudioSource matchingAudio;

    public Hangman hangman;
    public AudioSource hangmanAudio;
    public AudioClip hangmanNarration;

    private bool timing;
    // get audio source for hangman

    public MovePlayer movePlayer;

    public GameObject[] portals;
    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        timing = false;
        //SteamVR_Actions.move.Deactivate();
        SteamVR_Actions.move.Activate();
        timing = true;

        player.transform.position = shootingPosition;
        ufoGenerator.SetActive(true);

        //ufoGenerator.SetActive(false);

        mainRoomAudio.enabled = true;
        racingAudio.enabled = false;
        golf1Audio.enabled = false;
        golf2Audio.enabled = false;
        golf3Audio.enabled = false;
        shootingAudio.enabled = false;
        hangmanAudio.enabled = false;
        matchingAudio.enabled = false;

        portals = GameObject.FindGameObjectsWithTag("MainRoom");
        foreach(GameObject portal in portals)
        {
            portal.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Racing"))
        {
            timing = true;
            mainRoomAudio.enabled = false;
            racingAudio.enabled = true;

            PlayAudio(racingAudio, racingNarrationAudio);
            Debug.Log("Racing Triggered");
            SteamVR_Actions.move.Deactivate();

            player.transform.position = racingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            timing = true;
            mainRoomAudio.enabled = false;
            golf2Audio.enabled = true;

            Debug.Log("Golfing Triggered");

            PlayAudio(golf2Audio, golfNarrationAudio);

            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Hangman"))
        {
            timing = true;
            mainRoomAudio.enabled = false;
            hangmanAudio.enabled = true;

            Debug.Log("Hangman Triggered");

            PlayAudio(hangmanAudio, hangmanNarration);

            hangman.PickRandomWord();
            hangman.gameOver = false;
            player.transform.position = hangmanPosition;
            movePlayer.maxSpeed = 2.5f;
            // add enabled texts and audio below
        }
        else if (other.gameObject.CompareTag("Shooting"))
        {
            timing = true;
            mainRoomAudio.enabled = false;
            shootingAudio.enabled = true;

            Debug.Log("Shooting Triggered");

            PlayAudio(shootingAudio, shootingNarrationAudio);

            player.transform.position = shootingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            ufoGenerator.SetActive(true);

            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Matching"))
        {
            timing = true;
            mainRoomAudio.enabled = false;
            matchingAudio.enabled = true;

            Debug.Log("matching Triggered");

            PlayAudio(matchingAudio, matchingNarrationAudio);

            SteamVR_Actions.move.Deactivate();

            player.transform.position = matchingPosition;
            matchingGame.gameOver = false;
            matchingGame.sun.GetComponent<MeshRenderer>().material = matchingGame.good;
        } 
        else if (other.gameObject.CompareTag("MainRoom"))
        {
            SteamVR_Actions.move.Activate();

            timing = true;
            racingAudio.enabled = false;
            golf1Audio.enabled = false;
            golf2Audio.enabled = false;
            golf3Audio.enabled = false;
            shootingAudio.enabled = false;
            hangmanAudio.enabled = false;
            matchingAudio.enabled = false;
            mainRoomAudio.enabled = true;

            Debug.Log("Main Room Triggered");
            SteamVR_Actions.move.Activate();

            player.transform.position = startPosition;
            movePlayer.maxSpeed = 20.0f;

            foreach (GameObject portal in portals)
            {
                portal.SetActive(false);
            }
        }
    }

    public void PlayAudio(AudioSource source, AudioClip narration)
    {
        float timeNum = 0.0f;
        if (timeNum < 3.0f)
        {
            timeNum += Time.deltaTime;
        } else
        {
            source.PlayOneShot(narration);
        }
    }
}
