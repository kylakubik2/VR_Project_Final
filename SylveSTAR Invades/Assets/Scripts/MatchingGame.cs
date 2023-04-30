using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingGame : MonoBehaviour
{
    private MatchingCard[] cards;
    public int numFlipped;
    public GameObject[] theseCards;
    public int numInPair;
    // Start is called before the first frame update
    void Start()
    {
        cards = FindObjectsOfType<MatchingCard>();
        numFlipped = 0;
        numInPair = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (numFlipped == 2)
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
                }
            }
            numFlipped = 0;
        }
    }
}
