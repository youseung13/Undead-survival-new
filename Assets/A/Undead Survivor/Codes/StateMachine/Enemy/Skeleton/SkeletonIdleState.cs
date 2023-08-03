using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonIdleState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.SetZeroVelocity();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <0 && Vector2.Distance(enemy.transform.position,player.transform.position) > enemy.detectionRadius)
        stateMachine.ChangeState(enemy.patrolState);

        if(Vector2.Distance(enemy.transform.position,player.transform.position) <enemy.detectionRadius)
        stateMachine.ChangeState(enemy.moveState);


    }
}