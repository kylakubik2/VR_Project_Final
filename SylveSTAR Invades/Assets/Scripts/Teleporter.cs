using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Teleporter : MonoBehaviour
{
    // these Vector3's are "global" positions relative to the parent scene
    private Vector3 blackRoomPosition = new Vector3(-2272.43f, 5188.5f, 0f);
    private Vector3 startPosition = new Vector3(21.55f, 225.8f, -786.2f);
    private Vector3 racingPosition = new Vector3(-473.71f, 658.24f, -914.75f);
    private Vector3 golf1Position = new Vector3(219.635f, 505.296f, -1229.09f);
    private Vector3 golf2Position = new Vector3(244.818f, 512.73f, -1232.788f);
    private Vector3 golf3Position = new Vector3(255.01f, 519.74f, -1211.8f);
    private Vector3 hangmanPosition = new Vector3(0.44f, 9.76f, -3.5f);
    private Vector3 shootingPosition = new Vector3(633.57f, 411.0f, -354.45f);
    private Vector3 matchingPosition = new Vector3(353.19f, 939.21f, -428.32f);

    public GameObject player;

    public GameObject introButton;
    public GameObject outroButton;
    public GameObject creditButton;

    public GameObject shootingportal;
    public GameObject golfportal;
    public GameObject matchportal;
    public GameObject hangmanportal;
    public GameObject racingportal;
    public GameObject outroPortal;

    public AudioSource blackRoomAmbient;
    public AudioSource blackRoomNarration;
    public AudioSource blackRoomOutro;

    public AudioSource mainRoomAudio;
    public AudioSource mainRoomNarrationAudio;

    public finishLineScript racingGame;
    public AudioSource racingAudio;
    public AudioSource racingNarrationAudio;
    public AudioSource racingWinAudio;
    public AudioSource racingLoseAudio;
    public AudioClip racingNarration;
    public GameObject racingSign;
    public GameObject racingButton;
    public GameObject buggy;
    private Vector3 buggyStartPosition;

    public golfballcontroller golfGame1;
    public golfballcontroller golfGame3;
    public AudioSource golf1Audio;
    public AudioSource golf2Audio;
    public AudioSource golf3Audio;
    public AudioSource golfNarrationAudio;
    public GameObject golfButton;
    public AudioSource golf1WinAudio;
    public AudioSource golf1LoseAudio;
    public AudioSource golf3WinAudio;
    public AudioSource golf3LoseAudio;

    public LaserGunScript shootingGame;
    public GameObject ufoGenerator;
    public AudioSource shootingNarrationAudio;
    public AudioSource shootingAudio;
    public AudioSource shootingWinAudio;
    public AudioSource shootingLoseAudio;
    public GameObject shootingSign;
    public GameObject shootingButton;

    public MatchingGame matchingGame;
    public AudioSource matchingNarrationAudio;
    public AudioSource matchingAudio;
    public AudioSource matchingWinAudio;
    public GameObject matchingButton;
    public GameObject matchingSign;

    public Hangman hangman;
    public AudioSource hangmanAudio;
    public AudioSource hangmanNarration;
    public AudioSource hangmanWinAudio;
    public GameObject hangmanSign;
    public GameObject hangmanButton;

    public AudioClip whoosh;
    public AudioClip bigCelebrate;
    public AudioSource onPlayerAudio;
    // get audio source for hangman

    public MovePlayer movePlayer;

    public GameObject[] portals;

    public GameObject sylvestarOutro;
    private Vector3 sylvestarOutroStartPos;
    public SylvestarZoom sylvestarZoom;
    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        //SteamVR_Actions.move.Deactivate();
        SteamVR_Actions.move.Activate();

        introButton.SetActive(false);
        outroButton.SetActive(false);
        creditButton.SetActive(false);

        player.transform.position = blackRoomPosition;
        buggyStartPosition = buggy.transform.position;

        ufoGenerator.SetActive(false);

        //ufoGenerator.SetActive(false);
        blackRoomAmbient.enabled = true;
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
        shootingLoseAudio.enabled = false;

        hangmanAudio.enabled = false;
        hangmanNarration.enabled = false;
        hangmanWinAudio.enabled = false;

        matchingAudio.enabled = false;
        matchingNarrationAudio.enabled = false;
        matchingWinAudio.enabled = false;

        portals = GameObject.FindGameObjectsWithTag("MainRoom");
        foreach (GameObject portal in portals)
        {
            portal.SetActive(false);
        }

        outroPortal.SetActive(false);

        sylvestarOutroStartPos = sylvestarOutro.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (racingGame.hasWon)
        {
            if (golfGame1.hasWon || golfGame3.hasWon)
            {
                if (shootingGame.hasWon)
                {
                    if (matchingGame.haveWon)
                    {
                        if (hangman.hasWon)
                        {
                            shootingportal.SetActive(false);
                            golfportal.SetActive(false);
                            matchportal.SetActive(false);
                            hangmanportal.SetActive(false);
                            racingportal.SetActive(false);

                            outroPortal.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Racing"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            racingSign.SetActive(true);
            racingButton.SetActive(true);

            racingAudio.enabled = true;
            racingNarrationAudio.enabled = true;

            buggy.transform.position = buggyStartPosition;
            Debug.Log("Racing Triggered");
            SteamVR_Actions.move.Deactivate();

            racingGame.gameOver = false;
            player.transform.position = racingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            golf2Audio.enabled = true;
            golfNarrationAudio.enabled = true;
            player.transform.position = golf2Position;

            Debug.Log("Golfing Triggered");

            golfButton.SetActive(true);

            golfGame1.gameOver = false;
            golfGame3.gameOver = false;
            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Hangman"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            hangmanSign.SetActive(true);
            hangmanButton.SetActive(true);

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
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            shootingSign.SetActive(true);
            shootingButton.SetActive(true);

            shootingAudio.enabled = true;
            shootingNarrationAudio.enabled = true;

            Debug.Log("Shooting Triggered");

            shootingGame.gameOver = false;
            shootingGame.numHit = 0;
            player.transform.position = shootingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            
            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Matching"))
        {
            onPlayerAudio.PlayOneShot(whoosh);
            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;

            matchingAudio.enabled = true;
            matchingNarrationAudio.enabled = true;
            player.transform.position = matchingPosition;

            matchingSign.SetActive(true);
            matchingButton.SetActive(true);

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

            racingAudio.enabled = false;
            golf1Audio.enabled = false;
            golf2Audio.enabled = false;
            golf3Audio.enabled = false;
            shootingAudio.enabled = false;
            hangmanAudio.enabled = false;
            matchingAudio.enabled = false;

            hangmanWinAudio.enabled = false;
            shootingWinAudio.enabled = false;
            racingWinAudio.enabled = false;
            golf1WinAudio.enabled = false;
            golf3WinAudio.enabled = false;
            matchingWinAudio.enabled = false;

            shootingLoseAudio.enabled = false;
            racingLoseAudio.enabled = false;
            golf1LoseAudio.enabled = false;
            golf3LoseAudio.enabled = false;

            mainRoomAudio.enabled = true;
            mainRoomNarrationAudio.enabled = false;

            racingNarrationAudio.enabled = false;
            golfNarrationAudio.enabled = false;
            hangmanNarration.enabled = false;
            shootingNarrationAudio.enabled = false;
            matchingNarrationAudio.enabled = false;

            player.transform.position = startPosition;

            racingGame.gameOver = false;
            golfGame1.gameOver = false;
            golfGame3.gameOver = false;
            shootingGame.gameOver = false;
            matchingGame.gameOver = false;
            hangman.gameOver = false;

            Debug.Log("Main Room Triggered");
            SteamVR_Actions.move.Activate();

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

            racingAudio.enabled = false;
            golf1Audio.enabled = false;
            golf2Audio.enabled = false;
            golf3Audio.enabled = false;
            shootingAudio.enabled = false;
            hangmanAudio.enabled = false;
            matchingAudio.enabled = false;

            hangmanWinAudio.enabled = false;
            shootingWinAudio.enabled = false;
            racingWinAudio.enabled = false;
            golf1WinAudio.enabled = false;
            golf3WinAudio.enabled = false;
            matchingWinAudio.enabled = false;

            shootingLoseAudio.enabled = false;
            racingLoseAudio.enabled = false;
            golf1LoseAudio.enabled = false;
            golf3LoseAudio.enabled = false;

            racingNarrationAudio.enabled = false;
            golfNarrationAudio.enabled = false;
            hangmanNarration.enabled = false;
            shootingNarrationAudio.enabled = false;
            matchingNarrationAudio.enabled = false;

            mainRoomAudio.enabled = true;
            mainRoomNarrationAudio.enabled = true;

            racingGame.gameOver = false;
            golfGame1.gameOver = false;
            golfGame3.gameOver = false;
            shootingGame.gameOver = false;
            matchingGame.gameOver = false;
            hangman.gameOver = false;

            Debug.Log("Main Room Triggered");
            SteamVR_Actions.move.Activate();

            player.transform.position = startPosition;
            movePlayer.maxSpeed = 20.0f;

            foreach (GameObject portal in portals)
            {
                portal.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("Outro"))
        {
            sylvestarOutro.transform.position = sylvestarOutroStartPos;
            sylvestarZoom.zoom = false;

            SteamVR_Actions.move.Activate();
            onPlayerAudio.PlayOneShot(bigCelebrate, 0.5f);

            player.transform.position = blackRoomPosition;

            blackRoomAmbient.enabled = true;
            blackRoomOutro.enabled = true;

            mainRoomAudio.enabled = false;
            mainRoomNarrationAudio.enabled = false;
        }
    }
}
