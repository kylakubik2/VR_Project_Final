using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOGenerator : MonoBehaviour
{
    public Transform ufo;
    
    public float timeBeforeSpawning = 5.0f;
    public float timeBetweenUFOs = 0.25f;

    public Teleporter teleporter;
    public LaserGunScript laserScript;
    public bool startUFOs;
    public bool stopUFOs;

    void Start()
    {
        startUFOs = false;
        stopUFOs = false;
    }

    IEnumerator SpawnUFOs()
    {
        float randY;
        float randZ;
        float xPos = 649.664f;

        while (true)
        {
            randZ = Random.Range(-348.075f, -358.74f);
            randY = Random.Range(409.9f, 413.28f);
            Instantiate(ufo, new Vector3(xPos, randY, randZ), ufo.transform.rotation);
            
            randZ = Random.Range(-348.075f, -358.74f);
            randY = Random.Range(409.9f, 413.28f);
            Instantiate(ufo, new Vector3(xPos, randY, randZ), ufo.transform.rotation);
            
            yield return new WaitForSeconds(timeBetweenUFOs);
        }
    }

    void Update()
    {
        if (startUFOs)
        {
            StartCoroutine(SpawnUFOs());
            startUFOs = false;
        }
        if (stopUFOs)
        {
            StopCoroutine(SpawnUFOs());
            stopUFOs = false;
        }
    }
}