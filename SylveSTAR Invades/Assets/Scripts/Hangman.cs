using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangman : MonoBehaviour
{
    /**
     * ideas:
     *      only one key allowed at a time 
     *      player must enter (with enter key) to submit their letter before they can pick the next letter
     *      the game starts with just the hanging device (idk what it's actually called) and a number of dashed underlines
     *      the length of the word is the number of underlines
     *      the player enters one letter
     *      if the letter exists in the word, then for all instances of that letter, it will be put in places of the dash
     *      then the player can pick their next guess
     *      if the letter does not exist in the word, then part of the man will appear
     *          order: head, body, arm, arm, leg, leg, face, FAIL
     *      if the player completes the word without loosing, then they win; if they use all their attempts, then they lose
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
