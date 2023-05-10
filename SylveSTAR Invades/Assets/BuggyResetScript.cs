using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BuggyResetScript : MonoBehaviour
{
    public GameObject buggy;
    public Transform buggyResetPoint;
    private Interactable theBall;
    private Vector3 resetPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            buggy.transform.position = resetPoint;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        resetPoint = buggyResetPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
