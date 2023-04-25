using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidBoss : MonoBehaviour
{
    public int HP = 500; //Health of Slime
    public int DamageAmount = 40;

    public Animator animator;
    public Slider healthBar;

    void Update()
    {
        healthBar.value = HP; //allowing the health bar to move 
    }

    public void TakeDamage(int damageAmt)
    {
        HP -= damageAmt;
        if(HP <= 0)
        {
            //get damage animation, after it dies turn everything off   
            animator.SetTrigger("die");
            animator.SetBool("isAttack", false);
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", false);
            //remove mob from playing field
            Destroy(gameObject,3);
            GetComponent<Collider>().enabled = false;
        } 
        else
        {
            //get hit animation
            animator.SetTrigger("damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("This is a player collision.");
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
            health.TakeDamage(DamageAmount);
        }
    }
}
