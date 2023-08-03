using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawndata;
    public float levelTime;

    int level;
    float timer;

    float spawntimer;


    public string mapName;
    public GameObject enemyPrefab; 

     // assign enemy prefab in the inspector public 
     public Transform[] spawnPoints; 
     public int[] MaxenemyinSpot;
     public bool playerIn;
     public float spawnTime;
    // assign spawn points in the inspector 
     public int maxEnemies = 10;

    

    void Awake() 
    {
        spawnPoint = GetComponentsInChildren<Transform>();    
     //   levelTime = GameManager.instance.maxGameTime / spawndata.Length;
       
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isLive)
        return;

         timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            if(mapName == GameManager.instance.player.playerMap)
                {
                    if(GameManager.instance.numberOfenemy.Count <= (int)maxEnemies/2 )//전체갯수의 반이하가 되면 실행
                    {
                    spawntimer += Time.deltaTime;
                    if ( spawntimer >= spawnTime)
                        {
                           DoSpawn();
                            timer = 0;
                            spawntimer = 0;
                        }
                 
                    }
                }
        }
        /*

        timer += Time.deltaTime;
        level = Mathf.Min( Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawndata.Length - 1);//버림 + 인트화

        if(timer > spawndata[level].spawnTime)
        {
            timer = 0f;
            Spawn();
           
        }
        */
    }


    void Spawn()
    {
       
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = Vector2.zero;
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;//생성후 위치 지정
    //    enemy.GetComponent<Enemy>().Init(spawndata[level]);
        enemy.GetComponent<Enemy>().home =  enemy.transform.position;
        
    }



    private void DoSpawn()
     {
        for (int i = 0; i < spawnPoints.Length; i++) {
         // check if there are less than the maximum number of enemies at this spawn point 
            for(int j = 0; j< MaxenemyinSpot[i];j++)
            {
             // check if the spawn position is on an obstacle
                spawnPoints[i].position = new Vector3(spawnPoints[i].position.x+Random.Range(-1.5f,1.5f),spawnPoints[i].position.y+Random.Range(-1.5f,1.5f),spawnPoints[i].position.z);
                if (Physics2D.OverlapCircleAll(spawnPoints[i].position, 0.1f, LayerMask.GetMask("Obstacle")).Length > 0) 
                { // move the spawn position to a nearby location 
                     Debug.Log("hit Obstacle");
                    spawnPoints[i].position = new Vector3(spawnPoints[i].position.x - 0.3f, spawnPoints[i].position.y, spawnPoints[i].position.z); 
                   
                } // spawn an enemy at this spawn point 

                if(GameManager.instance.numberOfenemy.Count >= maxEnemies )
                {
                    return;
                }
                GameObject enemy = GameManager.instance.pool.Get2(Random.Range(0,2));
                enemy.transform.position = spawnPoints[i].position;
               // Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity); 
                GameManager.instance.numberOfenemy.Add(enemy);
                enemy.GetComponent<Enemy>().home =  enemy.transform.position;
               
            }
          
        } 
     }
}




[System.Serializable]
public class SpawnData
{
    public float spawnTime;
   public int sprtieType;
   public int health;
   public float speed;
   public int exp; 

  //  public Vector2 home;
}
