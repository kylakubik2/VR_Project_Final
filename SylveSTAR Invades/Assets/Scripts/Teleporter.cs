using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.0f, 3.0f, 0.0f);
    public Vector3 racingPosition = new Vector3(-595.58f, 21.11f, 247.71f); 
    public Vector3 golf1Position = new Vector3(-401.0f, 0.0f, 296.0f);
    public Vector3 golf2Position = new Vector3(-392.0f, 747.0f, 490.89f);
    public Vector3 golf3Position = new Vector3(-408.0f, 0.0f, 728.0f);

    public GameObject player;

    public AudioSource racingAudio;
    public TextMeshPro lapCountText;
    public TextMeshPro timerText;
    public TextMeshPro racingToBeat;

    public TextMeshPro parText;
    public TextMeshPro strokesText;

    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = startPosition;

        racingAudio.enabled = false;
        lapCountText.enabled = false;
        timerText.enabled = false;
        racingToBeat.enabled = false;

        parText.enabled = false;
        strokesText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Racing"))
        {
            Debug.Log("Racing Triggered");

            player.transform.position = racingPosition;
            racingAudio.enabled = true;

            racingAudio.enabled = true;
            lapCountText.enabled = true;
            timerText.enabled = true;
            racingToBeat.enabled = true;
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            Debug.Log("Golfing Triggered");

            int room = Random.Range(1, 4);
            Debug.Log(room);
            strokesText.enabled = true;
            parText.enabled = true;

            player.transform.position = golf2Position;
            parText.text = "Par: ";
        }
    }
}
