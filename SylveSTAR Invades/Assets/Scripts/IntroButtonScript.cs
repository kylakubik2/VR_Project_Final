using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroButtonScript : MonoBehaviour
{
    public GameObject mainPortal;
    public GameObject beginButton;
    public TextMeshPro introScript;
    public TextMeshPro outroScript;
    public GameObject outroButton;
    public AudioSource introAudio;
    public TextMeshPro title;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            mainPortal.SetActive(true);
            introScript.enabled = false;
            introAudio.Stop();
            outroScript.enabled = true;
            StartCoroutine(DelayButton());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        beginButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayButton()
    {
        yield return new WaitForSeconds(3.0f);
        outroButton.SetActive(true);
        title.enabled = true;
    }
}
