using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow =2;

    
    public PlayerPrimaryAttackState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        player.isattack = true;
        base.Enter();
        xInput = 0;

        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        comboCounter =0;

      //  Debug.Log(comboCounter);
        player.anim.SetInteger("ComboCounter", comboCounter);

        
        Vector2 attackDir = player.moveDirection;
        if(xInput != 0 || yInput != 0)
        attackDir = new Vector2(xInput,yInput);
      


       // player.SetVelocity(player.attackMovement[comboCounter]*attackDir.x, player.attackMovement[comboCounter]*attackDir.y);

        stateTimer = .1f;


        //애니메이트 속도제어
        //player.anim.speed =3;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor",.1f);

        comboCounter++;

        lastTimeAttacked = Time.time;
        player.isattack = false;

      
       // Debug.Log(lastTimeAttacked);


        //애니메이트 속도제어
        //player.anim.speed =1;
    }


    public override void Update()
    {
        base.Update();

        if(stateTimer <0)
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       //  player.SetVelocity(xInput * player.moveSpeed/5, yInput* player.moveSpeed/5);

        if(triggerCalled)
        stateMachine.ChangeState(player.idleState);
         
    }
}
