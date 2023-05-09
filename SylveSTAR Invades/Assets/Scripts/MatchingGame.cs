using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject button;

    //public AudioClip sunAudio;
    public AudioClip mercuryAudio;
    public AudioClip venusAudio;
    public AudioClip earthAudio;
    public AudioClip marsAudio;
    public AudioClip jupiterAudio;
    public AudioClip saturnAudio;
    public AudioClip uranusAudio;
    public AudioClip neptuneAudio;
    //public AudioClip plutoAudio;

    private AudioSource source;

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

            player.transform.SetParent(null);
        }

        if (numMatches == 8)
        {
            gameOver = true;
            haveWon = true;
        }

        if (haveWon)
        {
            cylinder.SetActive(false);
            button.SetActive(false);
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
                        gameOver = true;
                        sun.GetComponent<MeshRenderer>().material = bad;
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
                                    source.PlayOneShot(mercuryAudio, 0.5f);
                                    break;
                                case "venusCard":
                                    venus.SetActive(true);
                                    source.PlayOneShot(venusAudio, 0.5f);
                                    break;
                                case "earthCard":
                                    earth.SetActive(true);
                                    source.PlayOneShot(earthAudio, 0.5f);
                                    break;
                                case "marsCard":
                                    mars.SetActive(true);
                                    source.PlayOneShot(marsAudio, 0.5f);
                                    break;
                                case "jupiterCard":
                                    jupiter.SetActive(true);
                                    source.PlayOneShot(jupiterAudio, 0.5f);
                                    break;
                                case "saturnCard":
                                    saturn.SetActive(true);
                                    source.PlayOneShot(saturnAudio, 0.5f);
                                    break;
                                case "uranusCard":
                                    uranus.SetActive(true);
                                    source.PlayOneShot(uranusAudio, 0.5f);
                                    break;
                                case "neptuneCard":
                                    neptune.SetActive(true);
                                    source.PlayOneShot(neptuneAudio, 0.5f);
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
