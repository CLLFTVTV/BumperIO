using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 0;
    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Only spawn enemies if none
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber += 1;
            Spawn(waveNumber);
        }
    }

    void Spawn(int num)
    {
        //Spawn power up
        Instantiate(powerUp, GetSpawnPos(), Quaternion.identity);

        //Spawn +1 enemy per wave
        for (int i=0; i<num; i++)
        {
            Instantiate(enemy, GetSpawnPos(), Quaternion.identity);

            //Set random enemy scale and mass
            float scale = Random.Range(1, 3);
            enemy.transform.localScale = new Vector3(scale, scale, scale);
            enemy.gameObject.GetComponent<Rigidbody>().mass = scale;
        }
    }

    private Vector3 GetSpawnPos()
    {
        //Spawn at random position
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);

        return spawnPos;
    }
}
