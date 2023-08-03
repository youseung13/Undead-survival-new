using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
  protected Transform player;
  protected EnemyStateMachine stateMachine;
  protected Enemy2 enemyBase;
  protected Rigidbody2D rb;


  protected bool triggerCalled;
  private string animBoolName;
  protected float stateTimer;
  protected float stateTimer2;


  public EnemyState(Enemy2 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
  {
    this.enemyBase = _enemyBase;
    this.stateMachine = _stateMachine;
    this.animBoolName = _animBoolName;
  }

  public virtual void Update()
  {
    
    stateTimer -= Time.deltaTime;
     stateTimer2 -= Time.deltaTime;
  }

  public virtual void Enter()
  {
    triggerCalled =false;//트리거 이용해서 애니메이션 끝낼떄 필요
    rb = enemyBase.rb;
    enemyBase.anim.SetBool(animBoolName, true);

    player = PlayerManager.instance.player.transform;
  }

  public virtual void Exit()
  {
     enemyBase.anim.SetBool(animBoolName, false);
     enemyBase.AssignLastAnimName(animBoolName);
  }

  public virtual void AnimationFinishTrigger()
  {
    triggerCalled = true;
  }

  
}
