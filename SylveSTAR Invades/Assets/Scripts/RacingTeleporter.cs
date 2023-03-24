using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RacingTeleporter : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 racingPosition = new Vector3(-595.58f, 21.11f, 247.71f);

    public GameObject player;

    public AudioSource racingAudio;
    public TextMeshPro lapCountText;
    public TextMeshPro timerText;
    public TextMeshPro racingToBeat;

    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = startPosition;

        racingAudio.enabled = false;
        lapCountText.enabled = false;
        timerText.enabled = false;
        racingToBeat.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.position = racingPosition;
            racingAudio.enabled = true;

            racingAudio.enabled = true;
            lapCountText.enabled = true;
            timerText.enabled = true;
            racingToBeat.enabled = true;
        }
    }
}
