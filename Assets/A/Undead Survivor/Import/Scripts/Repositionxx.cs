using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repositionxx : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision) 
    {
        if(!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;//일단 플레이어 위치 저장
        Vector3 myPos = transform.position;//내위치 
        float diffX =Mathf.Abs(playerPos.x - myPos.x);//절대값 거리가 마이너스로 안되게
        float diffY =Mathf.Abs(playerPos.y - myPos.y);


      //  Vector3 playerDir = GameManager.instance.player.inputVec;
        // Vector3 playerDir = GameManager.instance.player.direction;
       // float dirX = playerDir.x < 0 ? -1 : 1;
        //float dirY = playerDir.y < 0 ? -1 : 1;


        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {//수평이동
              //      transform.Translate(Vector3.right*dirX*40);//40만큼 vector오른쪽으로 이동
                }
                else if (diffX < diffY)
                {//수직이동
               //     transform.Translate(Vector3.up*dirY*40);//40만큼 vector오른쪽으로 이동
                }
                break;
            case "Enemy":
                if(coll.enabled)//살아있으면 실행
                {
               //     transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));//플레이어가 가는방향에서 다시 등장    
                }
            
                break;
        }
    }
 
}
