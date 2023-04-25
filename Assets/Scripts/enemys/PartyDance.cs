using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartyDance : StateMachineBehaviour
{
    float timer;
    List<Transform> partywaypts = new List<Transform>();
    NavMeshAgent agent;

    Transform player;       //store and manipulate the position of player
    float chaseRange = 25;  //range to chase player

    public Transform cam;   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //find players location
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.2f;
        timer = 0;
        GameObject go = GameObject.FindGameObjectWithTag("partywaypts"); 
        foreach(Transform t in go.transform) //moves mob toward waypts location
            partywaypts.Add(t);
        agent.SetDestination(partywaypts[Random.Range(0, partywaypts.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(partywaypts[Random.Range(1, partywaypts.Count)].position);         
        
        timer += Time.deltaTime;
        if(timer > 5) //dance around the waypts for 5sec then go into idle state
            animator.SetBool("isPatrolling", false);
        //get the distance of player from the mobs
        float dist = Vector3.Distance(player.position, animator.transform.position);
        if(dist < chaseRange)
        {
            //set the chase animation script to true
            animator.SetBool("isChasing", true);
            //if chasing look at player
            animator.transform.LookAt(player);
        }
                    
    }    


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
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
