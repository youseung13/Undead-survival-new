using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playerxx : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }



    void Update() 
    {
        inputVec.x =Input.GetAxisRaw("Horizontal");
        inputVec.y =Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate() 
    {
        //1. 힘을준다
        //rigid.AddForce(inputVec);

        //2. 속도 제어
        //rigid.velocity = inputVec;

        //3. 위치이동
        Vector2 nextVec =inputVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate() 
    {
        anim.SetFloat("Speed", inputVec.magnitude);//벡터의길이


        if(inputVec.x !=0)
        {
            spriter.flipX = inputVec.x <0; 
        }
    }

/*
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
*/

}
