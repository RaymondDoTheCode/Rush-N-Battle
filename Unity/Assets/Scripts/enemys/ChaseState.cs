using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform target; //store and manipulate the position of player/target
    int rnd;
    
    void RandomN(ref int rnd)
    {
        rnd = Random.Range(1, 3);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        //find players location
        target = GameObject.Find("Player").transform;
        agent.speed = 5.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RandomN(ref rnd);
        //Debug.Log("this is the random#" + rnd);
        agent.SetDestination(target.position);
        float dist = Vector3.Distance(target.position, animator.transform.position);
        
        if(dist >= 26)
            //set the chase animation script to false putting mobs into walk state
            animator.SetBool("isChasing", false);
        else if(dist < 26){
            //set the chase animation script to true
             animator.SetBool("isChasing", true); 
             if(dist <= 2)
             {
                if(rnd == 1)
                {
                    //set the attack animation script to true
                    animator.SetBool("isAttack", true); 
                }
                else if(rnd == 2)
                {
                    //set the attack2 animation script to true
                    animator.SetBool("isAttack2", true); 
                }
             }
             
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
