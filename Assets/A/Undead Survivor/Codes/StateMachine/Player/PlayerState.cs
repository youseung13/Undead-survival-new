using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
   protected PlayerStateMachine stateMachine;
   protected Player2 player;
   protected Rigidbody2D rb;
   protected float xInput;
    protected float yInput;
   private string animBoolName;

   protected float stateTimer;
   protected bool triggerCalled;

   protected bool canmove =true;

   public PlayerState(Player2 _player,PlayerStateMachine _stateMachine, string _animBoolName)
   {
    this.player = _player;
    this.stateMachine = _stateMachine;
    this.animBoolName = _animBoolName;
   }

   public virtual void Enter()
   {
    player.anim.SetBool(animBoolName, true);
    rb = player.rb;
    triggerCalled = false;
   }

   public virtual void Update()
   {
    stateTimer -= Time.deltaTime;
   
   
     xInput = Input.GetAxisRaw("Horizontal");
     yInput = Input.GetAxisRaw("Vertical");

   }

   public virtual void Exit()
   {
    player.anim.SetBool(animBoolName, false);
   }

   public virtual void AnimationFinishTrigger()
   {
    triggerCalled=true;
   }

   public virtual bool HasNoSword()
   {
      if(!player.sword)
      {
         return true;
      }

      player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
      return false;
   }
}

