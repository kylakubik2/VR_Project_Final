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
    public TMP_Text wordText;
    public TMP_Text dashedText;

    //public AudioSource source;

    private const char PLACEHOLDER = '_';

    private int chances = 5;
    private int successes = 0;
    private int failures = 0;
    private string userInput;
    private string answer;

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

        wordText = GetComponent<TMP_Text>();
        dashedText = GetComponent<TMP_Text>();

        PickRandomWord();

    }

    // Update is called once per frame
    void Update()
    {
        if (failures > chances)
        {
            // you lose
            // wait five seconds
            player.transform.position = new Vector3(0.0f, 0.3f, 0.0f);
        }
        if(userInput.Equals(answer))
        {
            // you win
            // wait five seconds
            player.transform.position = new Vector3(0.0f, 0.3f, 0.0f);
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

    private void PickRandomWord()
    {
        int word = Random.Range(0, wordBank.Count);
        //wordText.text = wordBank[word];
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
