using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : StateMachineBehaviour
{
   Enemy enemy;
    Transform enemyTransform;

    float time;
    float rethinktime = 0.5f;

    public Vector2 facingDirection;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
     
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        
      //  animator.SetFloat("MoveX", facingDirection.x);
      //  animator.SetFloat("MoveY", facingDirection.y);

   
        if(Vector2.Distance(enemy.home, enemyTransform.position) <0.1f || Vector2.Distance(GameManager.instance.player.transform.position, enemyTransform.position) <= enemy.followrange + enemy.atkrange)
        {
            
            animator.SetBool("IsBack", false);
          
           
        }
        else
        {
            //enemy.DirectionEnemy(enemy.home.x, enemyTransform.position.x);
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position,enemy.home,Time.deltaTime*enemy.speed);
        }
       

       
      //  animator.SetFloat("MoveX", facingDirection.x);
     //   animator.SetFloat("MoveY", facingDirection.y);
    }

     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
