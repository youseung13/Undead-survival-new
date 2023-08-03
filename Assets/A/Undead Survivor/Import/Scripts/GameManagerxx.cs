using UnityEngine;
using System.Collections.Generic;

public class GameManagerxx : MonoBehaviour
{
    public static GameManagerxx instance = null;
    private List<List<int>> enemiesAtSpawnPoints; // keeps track of the number of enemies at each spawn point for each prefab
    private Transform[] spawnPoints;
    private GameObject[] enemyPrefabs;
    public int[] maxEnemies;

  
    private void Awake()
{
    if (instance == null)
        instance = this;
    else if (instance != this)
        Destroy(gameObject);
    DontDestroyOnLoad(gameObject);

   // enemiesAtSpawnPoints = new List<List<int>>();
}

private void Start() {
    
  //  BGMManager.instance.Play(0);
}

    public void SetSpawnPoints(Transform[] _spawnPoints)
    {
        spawnPoints = _spawnPoints;
        enemiesAtSpawnPoints = new List<List<int>>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemiesAtSpawnPoints.Add(new List<int>());
            for (int j = 0; j < enemyPrefabs.Length; j++)
            {
                enemiesAtSpawnPoints[i].Add(0);
            }
        }
    }
    public void SetEnemyPrefabs(GameObject[] _enemyPrefabs)
{
    enemyPrefabs = _enemyPrefabs;
    if (enemiesAtSpawnPoints == null)
    {
        enemiesAtSpawnPoints = new List<List<int>>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemiesAtSpawnPoints.Add(new List<int>());
            for (int j = 0; j < enemyPrefabs.Length; j++)
            {
                enemiesAtSpawnPoints[i].Add(0);
            }
        }
    }
}
    public void SetMaxEnemies(int[] _maxEnemies)
    {
        maxEnemies = _maxEnemies;
    }
    public int GetEnemiesAtSpawnPoint(int spawnPointIndex, int prefabIndex)
    {
        return enemiesAtSpawnPoints[spawnPointIndex][prefabIndex];
    }

    public void IncrementEnemiesAtSpawnPoint(int spawnPointIndex, int prefabIndex)
    {
        enemiesAtSpawnPoints[spawnPointIndex][prefabIndex]++;
    }
    public void DecrementEnemiesAtSpawnPoint(int spawnPointIndex, int prefabIndex)
    {
        enemiesAtSpawnPoints[spawnPointIndex][prefabIndex]--;
    }

     //public Player player;
    public PlayerController player;
    public PoolManager pool;

    public float gameTime;
    public float maxGameTime = 5 * 10f;
    //장면이 하나라서 굳이 싱ㄹ글톤으로 안함

      void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime; //소환하고 시간초기화
            
          
        }
    }  
   
}
