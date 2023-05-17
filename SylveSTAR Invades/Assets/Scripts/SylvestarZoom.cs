using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvestarZoom : MonoBehaviour
{
    public bool zoom;
    private Vector3 currentPosition;
    public Vector3 endPosition = new Vector3(-1490.3f, 5525.4f, -782.6f);
    private float smooth = 0.015f;

    public AudioSource vroom;

    // Start is called before the first frame update
    void Start()
    {
        zoom = false;
        vroom.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (zoom)
        {
            vroom.enabled = true;
            transform.position = Vector3.Lerp(currentPosition, endPosition, smooth * Time.deltaTime);
        }
    }
}
