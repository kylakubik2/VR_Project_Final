using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Teleporter : MonoBehaviour
{
    // these Vector3's are "global" positions relative to the parent scene
    private Vector3 blackRoomPosition = new Vector3(-2272.43f, 5189.5f, 0f);
    private Vector3 startPosition = new Vector3(21.55f, 225.8f, -769.35f);
    private Vector3 racingPosition = new Vector3(-473.71f, 658.24f, -914.75f);
    private Vector3 golf1Position = new Vector3(219.635f, 505.296f, -1229.09f);
    private Vector3 golf2Position = new Vector3(244.818f, 512.73f, -1232.788f);
    private Vector3 golf3Position = new Vector3(255.01f, 519.74f, -1211.8f);
    private Vector3 hangmanPosition = new Vector3(0.44f, 9.76f, -3.5f);
    private Vector3 shootingPosition = new Vector3(633.57f, 411.0f, -354.45f);
    private Vector3 matchingPosition = new Vector3(353.19f, 939.21f, -428.32f);

    public GameObject player;

    public AudioSource blackRoomAmbient;
    public AudioSource blackRoomNarration;

    public AudioSource mainRoomAudio;
    public AudioSource mainRoomNarrationAudio;

    public finishLineScript racingGame;
    public AudioSource racingAudio;
    public AudioSource racingNarrationAudio;
    public AudioClip racingNarration;
    
    public golfballcontroller golfGame1;
    public golfballcontroller golfGame3;
    public AudioSource golf1Audio;
    public AudioSource golf2Audio;
    public AudioSource golf3Audio;
    public AudioSource golfNarrationAudio;

    public LaserGunScript shootingGame;
    public GameObject ufoGenerator;
    public AudioSource shootingNarrationAudio;
    public AudioSource shootingAudio;
    public AudioSource shootingWinAudio;

    public MatchingGame matchingGame;
    public AudioSource matchingNarrationAudio;
    public AudioSource matchingAudio;

    public Hangman hangman;
    public AudioSource hangmanAudio;
    public AudioSource hangmanNarration;
    public AudioSource hangmanWinAudio;
    public GameObject hangmanSign;
    public GameObject hangmanButton;

    private bool delayTeleport;
    public AudioClip whoosh;
    public AudioSource onPlayerAudio;
    // get audio source for hangman

    public MovePlayer movePlayer;
    public float delayTime = 3.0f;
    private Vector3 newRotation;

    public GameObject[] portals;
    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        delayTeleport = false;
        //SteamVR_Actions.move.Deactivate();
        SteamVR_Actions.move.Activate();

        player.transform.position = startPosition;
        ufoGenerator.SetActive(false);

        //ufoGenerator.SetActive(false);
        blackRoomAmbient.enabled = false;
        blackRoomNarration.enabled = false;

        mainRoomAudio.enabled = false;
        mainRoomNarrationAudio.enabled = false;

        racingAudio.enabled = false;
        racingNarrationAudio.enabled = false;

        golf1Audio.enabled = false;
        golf2Audio.enabled = false;
        golf3Audio.enabled = false;
        golfNarrationAudio.enabled = false;


        shootingAudio.enabled = false;
        shootingNarrationAudio.enabled = false;
        shootingWinAudio.enabled = false;

        hangmanAudio.enabled = false;
        hangmanNarration.enabled = false;
        hangmanWinAudio.enabled = false;

        matchingAudio.enabled = false;
        matchingNarrationAudio.enabled = false;

        portals = GameObject.FindGameObjectsWithTag("MainRoom");
        foreach (GameObject portal in portals)
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
        if (other.gameObject.CompareTag("Racing"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            racingAudio.enabled = true;
            racingNarrationAudio.enabled = true;

            Debug.Log("Racing Triggered");
           // SteamVR_Actions.move.Deactivate();

            racingGame.gameOver = false;
            player.transform.position = racingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            delayTeleport = true;
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            //StartCoroutine(TeleportDelay);

            golf2Audio.enabled = true;
            golfNarrationAudio.enabled = true;
            player.transform.position = golf2Position;

            Debug.Log("Golfing Triggered");

            golfGame1.gameOver = false;
            golfGame3.gameOver = false;
            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Hangman"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            delayTeleport = true;
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            hangmanSign.SetActive(true);
            hangmanButton.SetActive(true);
            //StartCoroutine(TeleportDelay);

            hangmanAudio.enabled = true;
            hangmanNarration.enabled = true;

            Debug.Log("Hangman Triggered");

            hangman.PickRandomWord();
            hangman.gameOver = false;
            player.transform.position = hangmanPosition;
            movePlayer.maxSpeed = 2.5f;
        }
        else if (other.gameObject.CompareTag("Shooting"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            delayTeleport = true;
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            //StartCoroutine(TeleportDelay);

            shootingAudio.enabled = true;
            shootingNarrationAudio.enabled = true;

            Debug.Log("Shooting Triggered");

            shootingGame.gameOver = false;
            player.transform.position = shootingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            
            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Matching"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            delayTeleport = true;
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            //StartCoroutine(TeleportDelay);

            matchingAudio.enabled = true;
            matchingNarrationAudio.enabled = true;
            player.transform.position = matchingPosition;

            Debug.Log("matching Triggered");

            SteamVR_Actions.move.Deactivate();
            
            matchingGame.gameOver = false;
            matchingGame.sun.GetComponent<MeshRenderer>().material = matchingGame.good;
        }
        else if (other.gameObject.CompareTag("MainRoom"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            blackRoomAmbient.enabled = false;
            blackRoomNarration.enabled = false;

            SteamVR_Actions.move.Activate();

            delayTeleport = true;

            //StartCoroutine(TeleportDelay);

            racingAudio.enabled = false;
            golf1Audio.enabled = false;
            golf2Audio.enabled = false;
            golf3Audio.enabled = false;
            shootingAudio.enabled = false;
            hangmanAudio.enabled = false;
            matchingAudio.enabled = false;

            hangmanWinAudio.enabled = false;
            shootingWinAudio.enabled = false;

            mainRoomAudio.enabled = true;
            mainRoomNarrationAudio.enabled = false;

            racingNarrationAudio.enabled = false;
            golfNarrationAudio.enabled = false;
            hangmanNarration.enabled = false;
            shootingNarrationAudio.enabled = false;
            matchingNarrationAudio.enabled = false;

            Debug.Log("Main Room Triggered");
            SteamVR_Actions.move.Activate();

            player.transform.position = startPosition;
            movePlayer.maxSpeed = 20.0f;

            foreach (GameObject portal in portals)
            {
                portal.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("MainRoom1"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            blackRoomAmbient.enabled = false;
            blackRoomNarration.enabled = false;

            SteamVR_Actions.move.Activate();

            delayTeleport = true;

            //StartCoroutine(TeleportDelay);

            racingAudio.enabled = false;
            golf1Audio.enabled = false;
            golf2Audio.enabled = false;
            golf3Audio.enabled = false;
            shootingAudio.enabled = false;
            hangmanAudio.enabled = false;
            matchingAudio.enabled = false;

            hangmanWinAudio.enabled = false;
            shootingWinAudio.enabled = false;

            racingNarrationAudio.enabled = false;
            golfNarrationAudio.enabled = false;
            hangmanNarration.enabled = false;
            shootingNarrationAudio.enabled = false;
            matchingNarrationAudio.enabled = false;

            mainRoomAudio.enabled = true;
            mainRoomNarrationAudio.enabled = true;

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
}
