using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesToSpawn;

    //public Transform[] spanwPositions = new Transform[3];
    public Transform[] spanwPositions;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesToSpawn == 0)
        {
            CancelInvoke();
        }
    }

    void SpawnEnemy()
    {
        //Hace spanw de un enemigo en un punto aleatorio
        /*Transform selectedSpawn = spanwPositions[Random.Range(0, spanwPositions.Length)];

        Instantiate(enemyPrefab, selectedSpawn.position, selectedSpawn.rotation);*/

        foreach (Transform spawn in spanwPositions)
        {
            Instantiate(enemyPrefab, spawn.position, spawn.rotation);
        }

        /*
        for (int i = 0; i < spanwPositions.Length; i++)
        {
            Instantiate(enemyPrefab, spanwPositions[i].position, spanwPositions[i].rotation);
        }*/

        /*
        int i = 0;
        while (i < spanwPositions.Length)
        {
            Instantiate(enemyPrefab, spanwPositions[i].position, spanwPositions[i].rotation);
            i++;
        }*/

        /*int i = 0;
        do
        {
            Instantiate(enemyPrefab, spanwPositions[i].position, spanwPositions[i].rotation);
            i++;
        } while (i < spanwPositions.Length);*/

        enemiesToSpawn--;
    }
}
