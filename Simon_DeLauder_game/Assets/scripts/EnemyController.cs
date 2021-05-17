using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public GameObject target;
    bool isFolowing;
    Rigidbody2D rb;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;

        if (target.transform.position.x < transform.position.x && isFolowing)
        {
            velocity.x = -2f;
        }
        else if (isFolowing)
        {
            velocity.x = 2f;
        }

        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isFolowing = true;
        }
        else
        {
            isFolowing = false;
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        //Death fx go here --->
        FindObjectOfType<SpawnManager>().enemysOut--;
        Destroy(gameObject);
    }
}
