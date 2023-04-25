//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    public int HP = 60; //Health of Slime
    public int DamageAmount = 0;
    public Animator animator;

    public void TakeDamage(int damageAmt)
    {
        HP -= damageAmt;
        if(HP <= 0)
        {
            //get damage animation
            animator.SetTrigger("die");
            //remove mob from playing field
            Destroy(gameObject,1.5f);
            GetComponent<Collider>().enabled = false;
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
