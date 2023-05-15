using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollTextScript : MonoBehaviour
{
    //private Vector3 startPosition = new Vector3(19.3f, -7.46f, 172.49f);
    private Vector3 currentPosition;
    private Vector3 endPosition = new Vector3(-2272.31f, 5393.3f, 102.5f);
    private float smooth = 0.0075f;

    public AudioSource IntroScript;
    public TextMeshPro title;

    public bool moveText;
    // Start is called before the first frame update
    void Start()
    {
        moveText = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (moveText)
        {
            title.enabled = false;
            transform.position = Vector3.Lerp(currentPosition, endPosition, smooth * Time.deltaTime);
        }
    }
}
