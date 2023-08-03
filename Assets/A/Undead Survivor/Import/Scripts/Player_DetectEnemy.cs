using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DetectEnemy : MonoBehaviour
{
   public List<GameObject> FoundObjects;
    public GameObject enemy;
    public string TagName;
    public float shortDis;

    

    public Transform target;

    public float detectrate =1.5f;
    public float detecttime;
 
 
    void Start()
    {
          FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
 
    }

    private void Update()
    {
        detecttime += Time.deltaTime;

        if(detecttime > detectrate)
        {
            Detect();
            detecttime = 0;
        }
    }


    public void Detect()
    {
      
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // 첫번째를 기준으로 잡아주기 
 
        enemy = FoundObjects[0]; // 첫번째를 먼저 
 
        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
 
            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        Debug.Log(enemy.name);
        target = enemy.transform;
    }
}
