using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy2
{

    #region  States

    public SkeletonIdleState idleState {get; private set;}
    public SkeletonMoveState moveState {get; private set;}
    public SkeletonPatrolState patrolState {get; private set;}
    public SkeletonBattleState battleState {get; private set;}

     public SkeletonAttackState attackState {get; private set;}

     public SkeletonStunnedState stunnedstate { get; private set;}
     public SkeletonDeathState deadstate { get; private set;}
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        patrolState = new SkeletonPatrolState(this, stateMachine, "Patrol", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);

        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedstate = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadstate = new SkeletonDeathState(this, stateMachine, "Die",this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

    
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.O))
        stateMachine.ChangeState(stunnedstate);

      
    }

    public override bool CanBeStunned()
    {
       if(base.CanBeStunned())
       {
        stateMachine.ChangeState(stunnedstate);
        return true;
       }
       return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadstate);
    }
}
