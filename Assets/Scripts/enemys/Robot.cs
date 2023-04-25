using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    public int DamageAmount = 5;

    public float timeBetweenAttacks;
    bool alreadyAttacked = false;
    public GameObject projectile;

    public Animator animator;
    public Slider healthBar;
    public NavMeshAgent agent;

    Transform player;

    void Update()
    {
        healthBar.value = HP; //allowing the health bar to move
        if (animator.GetBool("isChasing") == true)
        {
            AttackPlayer();
        }
    }

    public void TakeDamage(int damageAmt)
    {
        HP -= damageAmt;
        if (HP <= 0)
        {
            //get damage animation, after it dies turn everything off            
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", false);
            //remove mob from playing field
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 3);
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // attack goes here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
