using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
 
    private Enemy_Skeleton enemy;

    private Vector2 moveDir;
    public SkeletonBattleState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("in battle state");

     
    }

    public override void Update()
    {
        base.Update();
        enemyBase.FlipController(player.transform.position.x -enemyBase.transform.position.x);

            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,
          player.transform.position, 
          Time.deltaTime*enemy.moveSpeed);

            if(Vector2.Distance(player.transform.position, enemy.transform.position)  < enemy.attackdistance)
            {
                Debug.Log("Attack player");
              //  enemy.SetZeroVelocity();
                return;
            }
            else if(enemy.IsPlayerDetected() == null)
            {
                stateMachine.ChangeState(enemy.idleState);
            }


     //   moveDir = new Vector2 (player.position.x - enemy.transform.position.x, player.position.y - enemy.transform.position.y);

      //  enemy.SetVelocity(enemy.moveSpeed, enemy.moveSpeed);
        
      //  enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, Time.deltaTime*enemy.moveSpeed);

    }


        public override void Exit()
    {
        base.Exit();
    }
}
