using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Teleporter : MonoBehaviour
{
    // these Vector3's are "global" positions relative to the parent scene
    private Vector3 startPosition = new Vector3(21.55f, 225.8f, -769.35f);
    private Vector3 racingPosition = new Vector3(-473.71f, 658.24f, -914.75f); 
    private Vector3 golf1Position = new Vector3(219.635f, 505.296f, -1229.09f);
    private Vector3 golf2Position = new Vector3(244.818f, 512.73f, -1232.788f);
    private Vector3 golf3Position = new Vector3(255.01f, 519.74f, -1211.8f);
    private Vector3 hangmanPosition = new Vector3(18.552f, 149.147f, -12.928f); 
    private Vector3 shootingPosition = new Vector3(-206.979f, 149.202f, -13.028f); 
    private Vector3 matchingPosition = new Vector3(353.19f, 939.21f, -428.32f);

    public GameObject player;
    
    public AudioSource racingAudio;
    public TextMeshPro lapCountText;
    public TextMeshPro timerText;
    public TextMeshPro racingToBeat;

    public AudioSource golf1Audio;
    public AudioSource golf2Audio;
    public AudioSource golf3Audio;
    public TextMeshPro parText;
    public TextMeshPro strokesText;
    public TextMeshPro golfWarmUp;

    public TextMeshPro numShot;
    public TextMeshPro ufoToBeat;
    public GameObject ufoGenerator;

    public MatchingGame matchingGame;
    public Hangman hangman;

    public MovePlayer movePlayer;

    public GameObject[] portals;
    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Actions.move.Activate();
        //SteamVR_Actions.move.Deactivate();

        player.transform.position = startPosition;
        ufoGenerator.SetActive(false);

        //ufoGenerator.SetActive(false);

        racingAudio.enabled = false;
        lapCountText.enabled = false;
        timerText.enabled = false;
        racingToBeat.enabled = false;

        parText.enabled = false;
        strokesText.enabled = false;
        golfWarmUp.enabled = false;
        golf1Audio.enabled = false;
        golf2Audio.enabled = false;
        golf3Audio.enabled = false;

        numShot.enabled = false;
        ufoToBeat.enabled = false;

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
            Debug.Log("Racing Triggered");
            SteamVR_Actions.move.Deactivate();

            player.transform.position = racingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
            racingAudio.enabled = true;

            racingAudio.enabled = true;
            lapCountText.enabled = true;
            timerText.enabled = true;
            racingToBeat.enabled = true;
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            Debug.Log("Golfing Triggered");

            golfWarmUp.enabled = true;
            golf2Audio.enabled = true;
            player.transform.position = golf2Position;
            parText.text = "Par: ";
            movePlayer.maxSpeed = 1.0f;
        }
        else if (other.gameObject.CompareTag("Hangman"))
        {
            Debug.Log("Hangman Triggered");

            hangman.PickRandomWord();
            hangman.gameOver = false;
            player.transform.position = hangmanPosition;
            movePlayer.maxSpeed = 1.0f;
            // add enabled texts and audio below
        }
        else if (other.gameObject.CompareTag("Shooting"))
        {
            Debug.Log("Shooting Triggered");

            player.transform.position = shootingPosition;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            ufoGenerator.SetActive(true);

            numShot.enabled = true;
            ufoToBeat.enabled = true;
            movePlayer.maxSpeed = 1.0f;
            // add enabled texts and audio below
        }
        else if (other.gameObject.CompareTag("Matching"))
        {
            Debug.Log("matching Triggered");
            SteamVR_Actions.move.Deactivate();

            player.transform.position = matchingPosition;
            matchingGame.gameOver = false;
            matchingGame.sun.GetComponent<MeshRenderer>().material = matchingGame.good;

            // add enabled texts and audio below
        } 
        else if (other.gameObject.CompareTag("MainRoom"))
        {
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
