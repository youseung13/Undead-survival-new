using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    Vector2 dashDirection;
    Vector3 dashDestination ;
    bool canDash;
    public PlayerDashState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

       // player.skill.clone.CreateClone(player.transform,Vector2.zero);//데쉬하면 클론생기게
        player.skill.clone.CreateCloneOnDashStart();
        stateTimer = player.dashDuration;
        canDash =true;
        canmove =true;
    }


    public override void Exit()
    {
        base.Exit();

        player.skill.clone.CreateCloneOnDashOver();

       // player.SetZeroVelocity();//공격중일떄 못움직이게
    }


public override void Update()
{
    base.Update();

    if(!canmove)
    return;

    player.FlipController(xInput);
    stateTimer -= Time.deltaTime;

    if (stateTimer > 0)
    {
        // Calculate the dash power
        dashDirection = new Vector2(xInput, yInput).normalized;
        dashDestination = player.transform.position + new Vector3(dashDirection.x, dashDirection.y) * player.dashRange;

        if(canDash)
        {
            DoDash();      
        }


    }

    // Change to the idle state once the dash duration is over
    if (stateTimer <= 0)
        stateMachine.ChangeState(player.idleState);
}



    public void DoDash()
    {
        player.transform.position = dashDestination;
        canDash = false;
    }
}
