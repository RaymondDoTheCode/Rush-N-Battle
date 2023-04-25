using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    public int explosionDamage;
    public float explosionRange;

    // Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    PhysicMaterial physics_mat;
    int collisions;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions)
        {
            Explode();
        }

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity); 
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<PlayerHealth>().TakeDamage(explosionDamage);
        }
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        collisions++;
        if (other.GetComponent<Collider>().CompareTag("Player") && explodeOnTouch)
        {
            Explode();
        }
    }

    private void Setup()
    {
        physics_mat = new PhysicMaterial();
    }
}
