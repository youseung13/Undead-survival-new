using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonMoveState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        enemyBase.FlipController(player.transform.position.x -enemyBase.transform.position.x);

        if  (Vector2.Distance(player.transform.position, enemy.transform.position)  > enemy.attackdistance &&
             Vector2.Distance(player.transform.position, enemy.transform.position)  < enemy.detectionRadius
            )
        {
                    
         enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,
          player.transform.position, 
          Time.deltaTime*enemy.moveSpeed*1.2f);
        }


        //enemy.SetVelocity(enemy.moveSpeed*enemy.facingDir, enemy.moveSpeed*enemy.facingDir);


          if(Vector2.Distance(player.transform.position, enemy.transform.position)  < enemy.attackdistance)
            {

                if(CanAttack())
                 {
                 stateMachine.ChangeState(enemy.attackState);

                 }

            }
            else if(Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.detectionRadius)
            {
                stateMachine.ChangeState(enemy.patrolState);
            }
          
           
        
    }


    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
      //  Debug.Log("Attack is on cooldown");

        return false;
    }
}
