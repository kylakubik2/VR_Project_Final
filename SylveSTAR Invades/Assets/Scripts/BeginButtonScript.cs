using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeginButtonScript : MonoBehaviour
{
    public RollTextScript rollText;
    public GameObject endIntroButton;
    public AudioSource introAudio;
    public TextMeshPro title;
    public SylvestarZoom sylvestarZoom;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            title.enabled = false;
            rollText.moveText = true;
            introAudio.enabled = true;
            StartCoroutine(DelayButton());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayButton()
    {
        sylvestarZoom.zoom = true;
        yield return new WaitForSeconds(1.5f);
        endIntroButton.SetActive(true);
    }
}
