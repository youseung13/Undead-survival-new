using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player2 _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        canmove = true;
    }
    public override void Exit()
    {
        base.Exit();
            //player.SetVelocity(0,0);
    }
    public override void Update()
    {
        base.Update();
        if(!canmove)
        return;
      
        if(Input.GetKeyDown(KeyCode.F5))
        stateMachine.ChangeState(player.blackHole);
     
        //rb.velocity = new Vector2(player.moveDirection.x * player.moveSpeed, player.moveDirection.y * player.moveSpeed);
        if((Input.GetKeyDown(KeyCode.T) ||Input.GetKeyDown(KeyCode.Mouse1)) && HasNoSword())
          {
     //공격중일떄 못움직이게
        stateMachine.ChangeState(player.aimSword);
          }

        if(Input.GetKeyDown(KeyCode.V) && player.isattack != true)
        stateMachine.ChangeState(player.primaryAttack);

        if(Input.GetKeyDown(KeyCode.Q) && player.isattack != true)
        stateMachine.ChangeState(player.counterAttack);

        //player.SetVelocity(xInput * player.moveSpeed, yInput* player.moveSpeed);
        player.transform.position += new Vector3(xInput * player.moveSpeed * Time.deltaTime, yInput * player.moveSpeed * Time.deltaTime, 0);
        player.FlipController(xInput);

        
        if(xInput == 0 && yInput ==0)
        stateMachine.ChangeState(player.idleState);

    }
}
