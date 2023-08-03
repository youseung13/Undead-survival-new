using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = .2f;
    private bool skillUsed;


    public PlayerBlackholeState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        skillUsed = false;
        stateTimer = flyTime;
    }
    public override void Exit()
    {
        base.Exit();

        player.fx.MakeTransprent(false);
    }

    public override void Update()
    {
        base.Update();

      //  if(stateTimer>0)
     //   rb.velocity=new Vector2(0, 15);

        if(stateTimer <0)
        {
          //  rb.velocity = new Vector2(0, -.1f);

            if(!skillUsed)
            {
                if(player.skill.blackhole.CanUseSkill())
                skillUsed = true;
            }
        }



        if(player.skill.blackhole.SkillCompleted())
        stateMachine.ChangeState(player.moveState);

    }
}
