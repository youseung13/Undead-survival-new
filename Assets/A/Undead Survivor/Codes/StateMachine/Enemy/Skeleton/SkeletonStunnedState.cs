/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    private float stunnedTime = 0.08f;
    public SkeletonStunnedState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy =_enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink",0, .1f);

        stateTimer = enemy.stunDuration;
        stateTimer2 = stunnedTime;

        
       // enemy.SetVelocity(-enemy.facingDir *enemy.stunDirction.x, -enemy.facingDir*enemy.stunDirction.y);
       rb.velocity = new Vector2(enemy.stunDirction.x* -enemy.facingDir*2, enemy.transform.position.normalized.y*-enemy.facingDir*3.5f);
      
    }
    
    public override void Update()
    {
        base.Update();



        if(stateTimer2 < 0)
        enemy.SetZeroVelocity();
       

        if(stateTimer <0)
        stateMachine.ChangeState(enemy.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelRedBlink",0);
    }

}

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonStunnedState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("CancelColorChange", 0, .1f);

        stateTimer = enemy.stunDuration;
        stateTimer2 = 0; // Reset stateTimer2

        
    }

    public override void Update()
    {
        base.Update();

        // Apply the knockback effect if it's still active
        if (stateTimer < 0)
        {
            // Change the state to the idle state (or another appropriate state)
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelRedBlink", 0);
    
    }
}




