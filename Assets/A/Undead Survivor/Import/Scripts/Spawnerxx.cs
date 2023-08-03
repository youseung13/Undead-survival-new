using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerxx : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        level =Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime /10f), spawnData.Length -1);//버리고 인트로

      ////  if(timer > spawnData[level].spawnTime)
        {
            timer =0f; //소환하고 시간초기화
            Spawn();
          
        }
    }    

       
       void Spawn()
       {
       
         GameObject enemy = GameManager.instance.pool.Get(0);
         enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;//생성함녓 위치도 지정
         enemy.GetComponent<NewEnemy>().Init(spawnData[level]);
       }
    
}



//[System.Serializable]
public class SpawnDataxx
{
    public float spawnTime;
   // public int spriteType;

    public int health;
    public float speed;
}
