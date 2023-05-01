using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCard : MonoBehaviour
{
    public Quaternion startRotation;
    public bool flipped;
    private float smooth = 100.0f;
    private float targetY;
    public MatchingGame game;
    

    void Start()
    {
        startRotation = transform.rotation;
        flipped = false;
        targetY = transform.localEulerAngles.y - 180.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wand")
        {
            if (!game.gameOver)
            {
                if (!flipped)
                {
                    Debug.Log("player touch card");
                    flipped = true;
                    game.numFlipped++;
                }
            }
        }
    }

    void Update()
    {
        if (flipped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetY, 0), Time.deltaTime * smooth);
        }
        if (!flipped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * smooth);
        }
    }
}
