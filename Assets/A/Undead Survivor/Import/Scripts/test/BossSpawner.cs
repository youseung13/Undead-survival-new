using System;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Vector3 spawnPosition;
    public float spawnInterval = 1f; //in minutes
    private float nextBossSpawnTime;
    private GameObject currentBoss;

    void Start()
    {
        // Set the initial spawn time
        nextBossSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if(!GameManager.instance.isLive)
        return;
        // Check if it's time to spawn the boss
        if (Time.time >= nextBossSpawnTime && currentBoss == null)
        {
            // Spawn the boss at the specified position
            currentBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            currentBoss.GetComponent<Enemy>().home =  currentBoss.transform.position;
            // Update the spawn time
            nextBossSpawnTime = Time.time + spawnInterval;
        }
        // Check if the current boss is still alive
        if (currentBoss != null && currentBoss.GetComponent<Enemy>().isDead == true)
        {
            currentBoss = null;
        }
    }
}