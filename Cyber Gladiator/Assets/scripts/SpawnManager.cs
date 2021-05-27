using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int roundNum = 1;
    public int enemysOut;
    public GameObject[] enemys;
    public GameObject[] spawners;
    public int roundStatus = 0;

    Stopwatch spawnRate = Stopwatch.StartNew();
    int randomSpawner;
    int enemyType;
    int maxEnemysOut = 30;
    int enemysRoundCount;
    int enemysToBeDeploid;

    private void Start()
    {
        //spawnRate.Start();
    }

    void Update()
    {
        switch (roundStatus)
        {
                //Standby Phase
            case 2:
                if (enemysOut <= 0)
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
                    if (spawnRate.ElapsedMilliseconds >= 300)
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
        spawnRate.Restart();
        enemysOut++;
        enemysToBeDeploid--;
        randomSpawner = Random.Range(0, spawners.Length);
        enemyType = Random.Range(0, enemys.Length);
        Instantiate(enemys[enemyType], spawners[randomSpawner].transform);
    }
}
