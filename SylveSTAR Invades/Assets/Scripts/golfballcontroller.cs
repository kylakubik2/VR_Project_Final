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
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 golf1Position = new Vector3(-401.0f, 748.5f, 272.35f);
    public Vector3 golf3Position = new Vector3(-427.6f, 748.5f, 708.62f);
    public Vector3 ballStartPosition = new Vector3();

    public TextMeshPro strokeCounter;
    public TextMeshPro parText;
    private int numStrokes;
    public float winTime = 100000.0f;
    public float teleport1Time = 100000.0f;
    public float teleport3Time = 100000.0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
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

            player.transform.position = startPosition;
            parText.enabled = false;
            strokeCounter.enabled = false;
        }
        if (Time.time > (teleport1Time + 3.0f))
        {
            player.transform.position = golf1Position;
            teleport1Time = 100000.0f;
        }
        if (Time.time > (teleport3Time + 3.0f))
        {
            player.transform.position = golf3Position;
            teleport3Time = 100000.0f;
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
        }
        else if (other.gameObject.CompareTag("Hole"))
        {
            winTime = Time.time;
            ball.transform.position = ballStartPosition;
        }
        else if (other.gameObject.CompareTag("Hole1"))
        {
            ball.transform.position = new Vector3(-401.6f, golf1Position.y + 1.78f, 268.0f);
            teleport1Time = Time.time;
        }
        else if (other.gameObject.CompareTag("Hole3"))
        {
            ball.transform.position = new Vector3(-424.4f, golf3Position.y + 1.78f, 709.1f);
            teleport3Time = Time.time;
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
