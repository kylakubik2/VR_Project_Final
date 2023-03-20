using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 racingPosition = new Vector3(-595.58f, 21.11f, 247.71f);
    public Vector3 golf1Position;
    public Vector3 golf2Position;
    public Vector3 golf3Position;

    // add other positions as we go
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
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
        }
        else if (other.gameObject.CompareTag("Golfing"))
        {
            int room = Random.Range(1, 4);
            if(room == 1)
            {
                transform.position = golf1Position;
            } else if (room == 2)
            {
                transform.position = golf2Position;
            }
            else
            {
                transform.position = golf3Position;
            }
        } else
        {
            Debug.Log("Adding other teleporation locations laterrrr");
        }
    }
}
