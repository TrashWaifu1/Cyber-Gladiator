using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int roundNum = 1;
    public int enemysOut;
    public GameObject[] enemys;
    public GameObject[] spawners;
    public int roundStatis;

    int randomSpawner;
    int enemyType;
    int maxEnemysOut = 30;
    int enemysRoundCount = 1;
    int enemysToBeDeploid;

    
    void Update()
    {
        if (roundStatis == 3)
        {
            roundNum++;
            enemysRoundCount += 5;
            enemysToBeDeploid = enemysRoundCount;
            roundStatis = 2;
        }

        if (roundStatis == 2)
        {
            
        }
    }

    void spawn()
    {
        randomSpawner = Random.Range(0, spawners.Length);
        enemyType = Random.Range(0, enemys.Length);
        Instantiate(enemys[enemyType], spawners[randomSpawner].transform);
    }
}
