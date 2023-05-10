using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using TMPro;

public class Hangman : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject player;
    public GameObject keyboard;
    public GameObject portal;

    public AudioSource source;
    public AudioClip music;
    public AudioClip click;
    public AudioClip write;
    public AudioClip draw;
    public AudioClip gameWin;
    public AudioClip gameLose;

    public TextMeshPro dashedText;

    public bool hasWon = false;

    //private Vector3 startPosition = new Vector3(3.45f, 1467.5f, -652.1f);
    private Transform[] keys;

    private const char PLACEHOLDER = '-';

    private int chances = 5;
    private int successes = 0;
    public int failures = 0;
    private string userInput;
    private string answer;
    public bool gameOver = false;

    public GameObject cylinder;
    public GameObject button;

    private List<string> wordBank = new List<string>() {"andromeda", "nebula", "kuiper", "constellation", "perihelion", "meteoroid", "rocket", "penumbra"};
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
        // ideas come from https://github.com/ivan-golubev/unityhangman/blob/master/
        // Start is called before the first frame update
    void Start()
    {
        // disable all hangman parts
        head.SetActive(false);
        body.SetActive(false);
        leftArm.SetActive(false);
        rightArm.SetActive(false);
        leftLeg.SetActive(false);
        rightLeg.SetActive(false);
        hasWon = false;

        dashedText.text = "-";

        source.Play();

        PickRandomWord();

        keys = keyboard.gameObject.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (failures > chances)
        {
            // you lose
            gameOver = true;
            hasWon = false;
            portal.SetActive(true);
            source.PlayOneShot(gameLose, 15.0f);

            head.SetActive(false);
            body.SetActive(false);
            leftArm.SetActive(false);
            rightArm.SetActive(false);
            leftLeg.SetActive(false);
            rightLeg.SetActive(false);

            foreach(Transform key in keys)
            {
                key.gameObject.SetActive(true);
            }

            failures = 0;
        }
        if (userInput.Equals(answer))
        {
            // you win
            gameOver = true;
            hasWon = true;
            portal.SetActive(true);
            source.PlayOneShot(gameWin, 15.0f);

            head.SetActive(false);
            body.SetActive(false);
            leftArm.SetActive(false);
            rightArm.SetActive(false);
            leftLeg.SetActive(false);
            rightLeg.SetActive(false);

            foreach (Transform key in keys)
            {
                key.gameObject.SetActive(true);
            }

            failures = 0;
        }

        if (hasWon)
        {
            cylinder.SetActive(false);
            button.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!gameOver)
        {
            string tag = other.gameObject.tag;
            if (tag.Length == 1)
            {
                char letter = tag.ToCharArray()[0]; //get the letter
                if (answer.Contains(letter))
                {
                    UpdateAnswerText(letter);
                    successes++;
                    source.PlayOneShot(click);
                    other.gameObject.SetActive(false);
                }
                else
                {
                    failures++;
                    DrawNextHangmanPart();
                    source.PlayOneShot(click);
                    other.gameObject.SetActive(false);
                }
            }
        }
    }

    public void PickRandomWord()
    {
        int word = Random.Range(0, wordBank.Count);
        answer = wordBank[word];
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++)
        {
            sb.Append(PLACEHOLDER);
        }
        dashedText.text = sb.ToString();
        userInput = sb.ToString();

    }

    private void UpdateAnswerText(char letter)
    {
        char[] inputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++)
        {
            if (inputArray[i] != PLACEHOLDER)
            {
                continue;
            }
            if (answer[i] == letter)
            {
                inputArray[i] = letter;
                source.PlayOneShot(write, 0.50f);
            }
        }
        userInput = new string(inputArray);
        dashedText.text = userInput;
    }

    
    private void DrawNextHangmanPart()
    {
        switch (failures) 
        {
            case 1:
                head.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            case 2:
                body.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            case 3:
                leftArm.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            case 4:
                rightArm.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            case 5:
                leftLeg.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            case 6:
                rightLeg.SetActive(true);
                source.PlayOneShot(draw, 1.0f);
                break;
            default:
                Debug.Log("??????????");
                break;
        }
    }
}
