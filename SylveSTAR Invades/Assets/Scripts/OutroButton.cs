using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutroButton : MonoBehaviour
{ 
    public GameObject creditsButton;
    public RollTextScript outroScript;
    public TextMeshPro title;
    public GameObject introButton;
    public SylvestarZoom sylvestarZoom;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            title.enabled = false;
            outroScript.moveText = true;
            StartCoroutine(DelayButton());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        introButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayButton()
    {
        sylvestarZoom.zoom = true;
        yield return new WaitForSeconds(1.5f);
        creditsButton.SetActive(true);
    }
}
