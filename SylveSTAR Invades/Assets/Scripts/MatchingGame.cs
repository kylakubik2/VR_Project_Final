using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MatchingGame : MonoBehaviour
{
    private MatchingCard[] cards;
    public int numFlipped;
    private GameObject[] theseCards;
    public GameObject portal;
    private int numInPair;
    public bool haveWon;
    public bool gameOver;
    public int numMatches;
    public GameObject player;

    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;
    public GameObject sun;

    public Material good;
    public Material bad;

    public GameObject cylinder;

    public AudioSource source;
    public AudioClip sunAudio;
    public AudioClip mercuryAudio;
    public AudioClip venusAudio;
    public AudioClip earthAudio;
    public AudioClip marsAudio;
    public AudioClip jupiterAudio;
    public AudioClip saturnAudio;
    public AudioClip uranusAudio;
    public AudioClip neptuneAudio;
    public AudioClip plutoAudio;

    public AudioSource gameWon;

    // Start is called before the first frame update
    void Start()
    {
        cards = FindObjectsOfType<MatchingCard>();
        numFlipped = 0;
        numInPair = 0;
        numMatches = 0;
        haveWon = false;
        gameOver = false;
       
        sun.SetActive(true);
        mercury.SetActive(false);
        venus.SetActive(false);
        earth.SetActive(false);
        mars.SetActive(false);
        jupiter.SetActive(false);
        saturn.SetActive(false);
        uranus.SetActive(false);
        neptune.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (numFlipped == 2)
        {
            Invoke("Matching", 3.0f);
            numFlipped = 0;
        }

        if (gameOver)
        {
            portal.SetActive(true);
            mercury.SetActive(false);
            venus.SetActive(false);
            earth.SetActive(false);
            mars.SetActive(false);
            jupiter.SetActive(false);
            saturn.SetActive(false);
            uranus.SetActive(false);
            neptune.SetActive(false);

            SteamVR_Actions.move.Activate();

            player.transform.SetParent(null);
            if (haveWon)
            {
                gameWon.enabled = true;
            }
        }

        if (numMatches == 8)
        {
            gameOver = true;
            haveWon = true;
        }

        if (haveWon)
        {
            cylinder.SetActive(false);
        }
    }

    public void Matching()
    {
        foreach (MatchingCard card in cards)
        {
            if (card.flipped)
            {
                theseCards = GameObject.FindGameObjectsWithTag(card.tag);
                foreach (GameObject thisCard in theseCards)
                {
                    if (thisCard.gameObject.GetComponent<MatchingCard>() == null)
                    {
                        continue;
                    }
                    else if (thisCard.gameObject.GetComponent<MatchingCard>().flipped)
                    {
                        numInPair++;
                    }
                }

                if (numInPair < 2)
                {
                    numInPair = 0;
                    card.flipped = false;
                }
                else
                {
                    if (card.tag == "plutoCard" || card.tag == "sunCard")
                    {
                        card.flipped = false;
                        gameOver = true;
                        sun.GetComponent<MeshRenderer>().material = bad;
                        if(card.tag == "plutoCard")
                        {
                            source.PlayOneShot(plutoAudio);
                        } else
                        {
                            source.PlayOneShot(sunAudio);
                        }
                    }
                    else
                    {
                        numMatches++;
                        foreach (GameObject thisCard in theseCards)
                        {
                            switch (thisCard.tag)
                            {
                                case "mercuryCard":
                                    mercury.SetActive(true);
                                    source.PlayOneShot(mercuryAudio);
                                    break;
                                case "venusCard":
                                    venus.SetActive(true);
                                    source.PlayOneShot(venusAudio);
                                    break;
                                case "earthCard":
                                    earth.SetActive(true);
                                    source.PlayOneShot(earthAudio);
                                    break;
                                case "marsCard":
                                    mars.SetActive(true);
                                    source.PlayOneShot(marsAudio);
                                    break;
                                case "jupiterCard":
                                    jupiter.SetActive(true);
                                    source.PlayOneShot(jupiterAudio);
                                    break;
                                case "saturnCard":
                                    saturn.SetActive(true);
                                    source.PlayOneShot(saturnAudio);
                                    break;
                                case "uranusCard":
                                    uranus.SetActive(true);
                                    source.PlayOneShot(uranusAudio);
                                    break;
                                case "neptuneCard":
                                    neptune.SetActive(true);
                                    source.PlayOneShot(neptuneAudio);
                                    break;
                                default:
                                    Debug.Log("???");
                                    break;
                            }

                            thisCard.SetActive(false);
                        }
                    }
                    numInPair = 0;
                }
            }
        }
    }
}
