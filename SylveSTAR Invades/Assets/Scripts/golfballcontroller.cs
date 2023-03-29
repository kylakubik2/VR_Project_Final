using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class golfballcontroller : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    public float clubForce = 1000.0f;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public TextMeshPro strokeCounter;
    public TextMeshPro parText;
    private int numStrokes;
    private float winTime = 10000.0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        numStrokes = 0;
        SetStrokeText();
    }

    void SetStrokeText()
    {
        strokeCounter.text = "Strokes: " + numStrokes.ToString();
    }

    void Update()
    {
        if (Time.time > (winTime + 5.0f))
        {
            player.transform.position = startPosition;
            parText.enabled = false;
            strokeCounter.enabled = false;
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
            gameObject.SetActive(false);
            winTime = Time.time;
        } else if (other.gameObject.CompareTag("Sand"))
        {
            rb.drag = 2; //slows down ball by increasing its drag
            // rb.AddForce(-(transform.position - other.gameObject.transform.position) * (clubForce - 5.0f)); //testing out to see if the AddForce works better than drag
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
