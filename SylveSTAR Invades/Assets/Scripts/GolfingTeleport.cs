using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GolfingTeleport : MonoBehaviour
{
    public Vector3 golf1Position = new Vector3(-401.0f, 0.0f, 296.0f);
    public Vector3 golf2Position = new Vector3(-444.0f, 0.0f, 446.0f);
    public Vector3 golf3Position = new Vector3(-408.0f, 0.0f, 728.0f);

    public GameObject player;
    public TextMeshPro parText;
    public TextMeshPro strokesText;
    //add audio soon

    // Start is called before the first frame update
    void Start()
    {
        parText.enabled = false;
        strokesText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int room = Random.Range(1, 4);
            Debug.Log(room);
            strokesText.enabled = true;
            parText.enabled = true;


            if (room == 1)
            {
                player.transform.position = golf1Position;
                parText.text = "Par: ";
            }
            else if (room == 2)
            {
                Debug.Log("got to part 2");
                player.transform.position = golf2Position;
                parText.text = "Par: ";
            }
            else
            {
                Debug.Log("got to part 3");
                player.transform.position = golf3Position;
                parText.text = "Par: ";
            }
        }
    }
}
