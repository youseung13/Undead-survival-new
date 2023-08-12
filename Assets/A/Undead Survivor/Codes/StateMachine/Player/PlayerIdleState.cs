using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       canmove = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.F5) && player.skill.blackhole.blackholeUnlocked)
        stateMachine.ChangeState(player.blackHole);

        if((Input.GetKeyDown(KeyCode.T)||Input.GetKeyDown(KeyCode.Mouse1)) && HasNoSword() && player.skill.sword.swordUnlocked)
        stateMachine.ChangeState(player.aimSword);

        if(Input.GetKeyDown(KeyCode.V) && player.isattack != true)
        stateMachine.ChangeState(player.primaryAttack);

        if(Input.GetKeyDown(KeyCode.Q) && player.isattack != true && player.skill.parry.parryUnlocked)
        stateMachine.ChangeState(player.counterAttack);

        if(!player.isBusy)
        {
             if(xInput !=0 || yInput !=0)
        stateMachine.ChangeState(player.moveState);
        }
       
    }
}
