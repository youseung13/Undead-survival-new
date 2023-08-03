using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    public WaitForSeconds wait = new WaitForSeconds(4f); // 지연 시간

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    private void Start()
    {
       
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;//왼쪽이 클래스 변수 의미
        this.per = per;


        if(per >= 0)
        {
            rigid.velocity = dir * 10f;//속도곱하기
             StartCoroutine(AutoDisable());
        }
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Enemy") || per == -100)
        return;

        
        per--;

        if(per < 0)
        {
            rigid.velocity = Vector2.zero;//다시 쓸꺼니까 물리 속도 초기화
            gameObject.SetActive(false);
            StopCoroutine(AutoDisable());
        }
    }


 


    private IEnumerator AutoDisable()
    {
        yield return wait; // WaitForSeconds를 사용하여 일정 시간만큼 대기
        gameObject.SetActive(false); // 게임 오브젝트를 비활성화
    }
}
