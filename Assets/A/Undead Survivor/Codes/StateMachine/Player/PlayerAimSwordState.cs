using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        canmove =false;

        player.skill.sword.DotsActive(true);
    }


    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }


    public override void Update()
    {
        base.Update();

        //player.SetZeroVelocity();//못움직이게

        if(Input.GetKeyUp(KeyCode.T) ||Input.GetKeyUp(KeyCode.Mouse1))
        stateMachine.ChangeState(player.idleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(player.transform.position.x > mousePosition.x && player.facingDir ==1)
        player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
        player.Flip();
    }
}
