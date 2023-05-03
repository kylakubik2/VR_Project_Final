using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class golfballcontroller : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    public GameObject ball;
    public float clubForce = 1000.0f;
    public bool hasWon = false;
    public GameObject portal;

    private Vector3 startPosition = new Vector3(4.0f, 1467.5f, -651.0f);
    public Vector3 golf1Position = new Vector3(-401.0f, 748.5f, 272.35f);
    public Vector3 golf3Position = new Vector3(-427.6f, 748.5f, 708.62f);
    public Vector3 ballStartPosition = new Vector3();

    public TextMeshPro strokeCounter;
    public TextMeshPro parText;
    private int numStrokes;
    // TODO: set sylvestarPar
    private int sylvestarPar;
    public float winTime = 100000.0f;
    public float teleport1Time = 100000.0f;
    public float teleport3Time = 100000.0f;

    public AudioSource golf1Source;
    public AudioSource golf2Source;
    public AudioSource golf3Source;

    public AudioClip boing;

    public TextMeshPro golfWarmUp;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //portal.SetActive(false);
        hasWon = false;
        rb = GetComponent<Rigidbody>();
        numStrokes = 0;
        SetStrokeText();
        ballStartPosition = ball.transform.position;
    }

    void SetStrokeText()
    {
        strokeCounter.text = "Strokes: " + numStrokes.ToString();
    }

    void Update()
    {
        if (Time.time > (winTime + 5.0f))
        {
            Debug.Log("WHOOP");

            //player.transform.position = startPosition;
            numStrokes = 0;
            parText.enabled = false;
            strokeCounter.enabled = false;
            golf1Source.enabled = false;
            golf3Source.enabled = false;
        }
        if (Time.time > (teleport1Time + 3.0f))
        {
            player.transform.position = golf1Position;
            golf1Source.enabled = true;
            teleport1Time = 100000.0f;
            strokeCounter.enabled = true;
            parText.enabled = true;
            numStrokes = 0;
            SetStrokeText();
        }
        if (Time.time > (teleport3Time + 3.0f))
        {
            player.transform.position = golf3Position;
            golf3Source.enabled = true;
            teleport3Time = 100000.0f;
            strokeCounter.enabled = true;
            parText.enabled = true;
            numStrokes = 0;
            SetStrokeText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Club"))
        {
            // By subtracting the transform of the club off of the transform of the ball we find the vector from the club to the ball. Then apply a force in that direction to push the ball that way!
            rb.AddForce((transform.position - other.gameObject.transform.position) * clubForce);
            numStrokes++;
            SetStrokeText();
            golf1Source.PlayOneShot(boing);
            golf2Source.PlayOneShot(boing);
            golf3Source.PlayOneShot(boing);
        }
        else if (other.gameObject.CompareTag("Hole"))
        {
            winTime = Time.time;
            ball.transform.position = ballStartPosition;
        }
        else if (other.gameObject.CompareTag("Hole1"))
        {
            ball.transform.position = ballStartPosition;
            teleport1Time = Time.time;
            golfWarmUp.enabled = false;
            golf2Source.enabled = false;
            if (numStrokes < sylvestarPar)
            {
                hasWon = true;
                portal.SetActive(true);
            } else
            {
                hasWon = false;
                portal.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Hole3"))
        {
            ball.transform.position = ballStartPosition;
            teleport3Time = Time.time;
            golfWarmUp.enabled = false;
            golf2Source.enabled = false;
            if (numStrokes < sylvestarPar)
            {
                hasWon = true;
                portal.SetActive(true);
            } else
            {
                hasWon = false;
                portal.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Sand"))
        {
            rb.drag = 2; //slows down ball by increasing its drag
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sand"))
        {
            rb.drag = 0;
        }
    }
}
