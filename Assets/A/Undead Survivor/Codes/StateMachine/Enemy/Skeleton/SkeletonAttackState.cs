using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonAttackState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
       // enemy.SetZeroVelocity();

        if(triggerCalled)
        stateMachine.ChangeState(enemy.moveState);
    }

       public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;

    }
}
