/*
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

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <0)
        stateMachine.ChangeState(enemy.moveState);

        if(enemy.IsPlayerDetected())
        stateMachine.ChangeState(enemy.battleState);


    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolState : EnemyState
{
    private Enemy_Skeleton enemy;
    private Vector2 patrolEndPos;
    private float maxPatrolRange = 4f; // Maximum patrol range
    private float patrolTimer = 0f;

    private float patrolDelay = 1.5f; // Adjust this value to set the delay time between patrols.

    private bool isPatrolling = true;

    public SkeletonPatrolState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // Set up patrol start position at the current enemy position.
        patrolEndPos = enemy.homePos + new Vector2(Random.Range(-maxPatrolRange, maxPatrolRange), Random.Range(-maxPatrolRange, maxPatrolRange));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        enemyBase.FlipController(patrolEndPos.x -enemyBase.transform.position.x);
        base.Update();
        patrolTimer += Time.deltaTime;
        // Continuously check the distance to the player.
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.transform.position);

        // Transition to battle state if the player gets close enough.
        if(Vector2.Distance(enemy.transform.position,player.transform.position) <enemy.detectionRadius)
        {
            stateMachine.ChangeState(enemy.moveState);
            return;
        }
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, patrolEndPos, enemy.moveSpeed * Time.deltaTime);
              //  Debug.Log("enpos is" + new Vector2(patrolEndPos.x , patrolEndPos.y));
            if(Vector2.Distance(enemy.transform.position, patrolEndPos)< 0.1f)
            {
                    stateMachine.ChangeState(enemy.idleState);
                    
            }

        
    }
}
