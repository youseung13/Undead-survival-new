/* 
EnemySpawner.cs
using UnityEngine;
using System.Collections;

public class NewSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // assign enemy prefabs in the inspector
    public Transform[] spawnPoints; // assign spawn points in the inspector
    public int[] maxEnemies; // maximum number of enemies per spawn point for each prefab
    public float spawnInterval = 20.0f; // interval at which to spawn new enemies

   private void Start()
{
    if(spawnPoints != null && enemyPrefabs != null && maxEnemies != null)
    {
        GameManager.instance.SetSpawnPoints(spawnPoints);
        GameManager.instance.SetEnemyPrefabs(enemyPrefabs);
        GameManager.instance.SetMaxEnemies(maxEnemies);
        // start the spawn coroutine
        StartCoroutine(SpawnEnemies());
    }
}

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // loop through each spawn point
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                // loop through each enemy prefab
                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    // check if there are less than the maximum number of enemies at this spawn point for this prefab
                    if (maxEnemies != null && GameManager.instance.GetEnemiesAtSpawnPoint(i, j) < maxEnemies[j])
                    {
                        // check if the spawn position is on an obstacle
                        if (Physics2D.OverlapCircleAll(spawnPoints[i].position, 0.1f, LayerMask.GetMask("Obstacle")).Length > 0)
                        {
                            // move the spawn position to a nearby location
                            spawnPoints[i].position = new Vector3(spawnPoints[i].position.x - 1, spawnPoints[i].position.y, spawnPoints[i].position.z);
                        }

                        // spawn an enemy at this spawn point
                        GameObject newEnemy = Instantiate(enemyPrefabs[j], spawnPoints[i].position, Quaternion.identity);
                        newEnemy.GetComponent<NewEnemy>().spawnPointIndex = i;
                        newEnemy.GetComponent<NewEnemy>().prefabIndex = j;
                        GameManager.instance.IncrementEnemiesAtSpawnPoint(i, j);
                    }
                }
            }
            // wait for the spawn interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
*/


using System.Collections.Generic;
using UnityEngine;

 public class NewSpawner : MonoBehaviour {
     public GameObject enemyPrefab; 

     // assign enemy prefab in the inspector public 
     public Transform[] spawnPoints; 
     public int[] MaxenemyinSpot;

    
   

     public bool playerIn;

     public float spawnTime;
     public float timer;
     // assign spawn points in the inspector 
     public int maxEnemies = 5; 
     // maximum number of enemies per spawn point 
    private void Awake() {
   
    }
     private void Start() {
        
        
        DoSpawn();
        
     }
     private void Update() 
     { // loop through each spawn point

     timer += Time.deltaTime;
     if(timer >= spawnTime)
     {
        DoSpawn();
        timer = 0;
     }

      
     } 


     private void DoSpawn()
     {
        for (int i = 0; i < spawnPoints.Length; i++) {
         // check if there are less than the maximum number of enemies at this spawn point 
            for(int j = 0; j< MaxenemyinSpot[i];j++)
            {
             // check if the spawn position is on an obstacle
                spawnPoints[i].position = new Vector3(spawnPoints[i].position.x+Random.Range(-3f,3f),spawnPoints[i].position.y+Random.Range(-3f,3f),spawnPoints[i].position.z);
                if (Physics2D.OverlapCircleAll(spawnPoints[i].position, 0.1f, LayerMask.GetMask("Obstacle")).Length > 0) 
                { // move the spawn position to a nearby location 
                     Debug.Log("hit Obstacle");
                    spawnPoints[i].position = new Vector3(spawnPoints[i].position.x - 1, spawnPoints[i].position.y, spawnPoints[i].position.z); 
                   
                } // spawn an enemy at this spawn point 


            ////    GameObject enemy = GameManager.instance.pool.Get(0);
            ////    enemy.transform.position = spawnPoints[i].position;
               // Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity); 
               
            }
          
        } 
     }
}