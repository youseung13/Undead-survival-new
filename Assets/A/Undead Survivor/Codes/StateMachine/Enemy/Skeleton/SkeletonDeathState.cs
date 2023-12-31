using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeathState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonDeathState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

    //    enemy.anim.SetBool(enemy.lastAnimBoolName,true);
    //    enemy.anim.speed=0;
        enemy.cd.enabled =false;

        stateTimer = .1f;
        Debug.Log("enter dead state");
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
