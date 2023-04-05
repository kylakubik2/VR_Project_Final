using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public Transform ufo;

    public float timeBeforeSpawning = 2.0f;
    public float timeBetweenUFOs = 5.0f;

    public Teleporter teleporter;
    public LaserGunScript laserScript;

    public int countUFOs = 0;

    void Start()
    {
        
    }

    IEnumerator SpawnUFOs()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);

        float randY;
        float randZ;
        float xPos = -242.95f;

        while (!laserScript.stopUFO)
        {
            randZ = Random.Range(220.61f, 266.98f);
            randY = Random.Range(5.0f, 27.12f);
            Instantiate(ufo, new Vector3(xPos, randY, randZ), ufo.transform.rotation);
            countUFOs++;
            yield return new WaitForSeconds(timeBetweenUFOs);
            
        }
    }

    void Update()
    {
        if (teleporter.inShoot)
        {
            StartCoroutine(SpawnUFOs());
        }
    }
}
