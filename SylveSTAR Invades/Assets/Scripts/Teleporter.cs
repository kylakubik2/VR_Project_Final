using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 racingPosition = new Vector3(-595.58f, 21.11f, 247.71f);
    public Vector3 golf1Position = new Vector3(0.27f, 0.0f, 1.0f);
    public Vector3 golf2Position = new Vector3(-3.705f, 0.0f, -1.0f);
    public Vector3 golf3Position = new Vector3(-1.3f, 0.0f, -1.2f);

    public AudioSource racingAudio;
    public TextMeshPro lapCountText;
    public TextMeshPro timerText;
    public TextMeshPro racingToBeat;

    public TextMeshPro strokesText;
    public TextMeshPro parText;

    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;

        racingAudio.enabled = false;
        lapCountText.enabled = false;
        timerText.enabled = false;
        racingToBeat.enabled = false;

        strokesText.enabled = false;
        parText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Racing"))
        {
            transform.position = racingPosition;
            racingAudio.enabled = true;

            racingAudio.enabled = true;
            lapCountText.enabled = true;
            timerText.enabled = true;
            racingToBeat.enabled = true;
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            int room = Random.Range(1, 4);
            strokesText.enabled = true;
            parText.enabled = true;

            if (room == 1)
            {
                transform.position = golf1Position;
            } 
            else if (room == 2)
            {
                transform.position = golf2Position;
            }
            else
            {
                transform.position = golf3Position;
            }
        } 
        else
        {
            Debug.Log("Adding other teleporation locations laterrrr");
        }
    }
}
