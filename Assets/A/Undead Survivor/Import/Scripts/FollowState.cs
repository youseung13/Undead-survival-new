using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{
    Enemy enemy;
    Transform enemyTransform;

    float delay = 0.3f;

    float time;

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
        time += Time.deltaTime;
        if(time > delay )
        {
            if(Vector2.Distance(GameManager.instance.player.transform.position, enemyTransform.position) >enemy.atkrange &&
            Vector2.Distance(GameManager.instance.player.transform.position, enemyTransform.position) < enemy.followrange +enemy.atkrange)
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position,GameManager.instance.player.transform.position, Time.deltaTime*enemy.speed);

            else if(Vector2.Distance(GameManager.instance.player.transform.position, enemyTransform.position) > enemy.followrange + enemy.atkrange)
            {
                animator.SetBool("IsBack", true);
                animator.SetBool("IsFollow", false);
            }
            else
            {
                animator.SetBool("IsBack", false);
                animator.SetBool("IsFollow", false);
            }
        }
          

       // facingDirection = enemy.player.position - enemyTransform.position;
       // animator.SetFloat("MoveX", facingDirection.x);
      //  animator.SetFloat("MoveY", facingDirection.y);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        
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
