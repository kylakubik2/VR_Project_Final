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

    //public AudioSource music;
    //public AudioSource click;
    //public AudioSource write;
    //public AudioSource draw;

    public TextMeshPro wordText;
    public TextMeshPro dashedText;

    public bool hasWon = false;

    //private Vector3 startPosition = new Vector3(3.45f, 1467.5f, -652.1f);
    private Transform[] keys;

    private const char PLACEHOLDER = '-';

    private int chances = 5;
    private int successes = 0;
    private int failures = 0;
    private string userInput;
    private string answer;
    private bool gameOver = false;


    private List<string> wordBank = new List<string>() {"andromeda", "nebula", "kuiper", "constellation", "perihelion"};
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

        wordText.text = "Letters Guessed";
        dashedText.text = "-";

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
            //player.transform.position = startPosition;
          
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
        }
        if (userInput.Equals(answer))
        {
            // you win
            gameOver = true;
            hasWon = true;
            //player.transform.position = startPosition;

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
        }

        if (gameOver)
        {
            portal.SetActive(true);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if(tag.Length == 1)
        {
            char letter = tag.ToCharArray()[0]; //get the letter
            if (answer.Contains(letter))
            {
                UpdateAnswerText(letter);
                successes++;
                other.gameObject.SetActive(false);
            }
            else
            {
                failures++;
                DrawNextHangmanPart();
                other.gameObject.SetActive(false);
            }
        }
    }

    public void PickRandomWord()
    {
        int word = Random.Range(0, wordBank.Count);
        //wordText.text = wordBank[word];
        answer = wordBank[word];
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++)
        {
            sb.Append(PLACEHOLDER);
            //sb.Append(" ");
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
                break;
            case 2:
                body.SetActive(true);
                break;
            case 3:
                leftArm.SetActive(true);
                break;
            case 4:
                rightArm.SetActive(true);
                break;
            case 5:
                leftLeg.SetActive(true);
                break;
            case 6:
                rightLeg.SetActive(true);
                break;
            default:
                Debug.Log("??????????");
                break;
        }
    }
}
