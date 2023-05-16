using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsButtonScript : MonoBehaviour
{
    public TextMeshPro outroText;
    public RollTextScript credits;
    public GameObject outroButton;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            outroText.enabled = false;
            credits.moveText = true;
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        outroButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
