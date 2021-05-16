using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int roundNum = 1;
    public int enemysOut = 0;
    public GameObject[] enemys;
    public GameObject[] spawners;
    public int roundStatus = 0;

    int randomSpawner;
    int enemyType;
    int maxEnemysOut = 30;
    int enemysRoundCount = 1;
    int enemysToBeDeploid = 1;

    void Update()
    {
        switch (roundStatus)
        {
                //Standby Phase
            case 2:
                if (enemysOut == 0)
                    roundStatus = 1;
                break;
                //Reset Phase
            case 1:
                roundReset();
                break;
                //Spawning Phase
            default:
                if (enemysOut < maxEnemysOut)
                {
                    spawn();
                    if (enemysToBeDeploid == 0)
                        roundStatus = 2;
                }
                break;
        }
    }

    void roundReset()
    {
        roundNum++;
        enemysRoundCount += 5;
        enemysToBeDeploid = enemysRoundCount;
        roundStatus = 0;
    }

    void spawn()
    {
        enemysOut++;
        enemysToBeDeploid--;
        randomSpawner = Random.Range(0, spawners.Length);
        enemyType = Random.Range(0, enemys.Length);
        Instantiate(enemys[enemyType], spawners[randomSpawner].transform);
    }
}
