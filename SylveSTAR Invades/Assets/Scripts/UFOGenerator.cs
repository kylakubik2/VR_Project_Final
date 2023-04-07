using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOGenerator : MonoBehaviour
{
    public Transform ufo;

    public float timeBeforeSpawning = 2.0f;
    public float timeBetweenUFOs = 5.0f;

    public Teleporter teleporter;
    public LaserGunScript laserScript;

    void Start()
    {
        StartCoroutine(SpawnUFOs());
    }

    IEnumerator SpawnUFOs()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);

        float randY;
        float randZ;
        float xPos = -212.31f;

        while (true)
        {
            randZ = Random.Range(169.09f, 173.23f);
            randY = Random.Range(0.647f, 2.31f);
            Instantiate(ufo, new Vector3(xPos, randY, randZ), ufo.transform.rotation);
            yield return new WaitForSeconds(timeBetweenUFOs);
        }
    }

    void Update()
    {
        
    }
}