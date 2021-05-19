using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public float speed = 10;
    public float maxSpeed = 5;
    public float bounceForce = 5;
    public GameObject[] items;

    bool isFollowing;
    Rigidbody2D rb;
    Vector2 velocity;
    GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (isFollowing)
            rb.AddForce(new Vector2((target.transform.position.x < transform.position.x ? (rb.velocity.x > -maxSpeed ? -speed : 0) : (rb.velocity.x < maxSpeed ? speed : 0)) * Time.deltaTime, 0), ForceMode2D.Impulse);
    }

    //Attack
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().TakeDamage((int)Mathf.Abs(rb.velocity.x * 10));
            collision.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((rb.velocity.x < 0 ? -bounceForce : bounceForce) * 2, 0), ForceMode2D.Impulse);
            rb.AddForce(new Vector2((rb.velocity.x > 0 ? -bounceForce : bounceForce), bounceForce * .6f), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            isFollowing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            isFollowing = false;
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
        spawnItem();
        FindObjectOfType<SpawnManager>().enemysOut--;
        Destroy(gameObject);
    }

    void spawnItem()
    {
        if (Random.Range(0, 3) == 0)
            Instantiate(items[Random.Range(0, items.Length)], gameObject.transform.position, gameObject.transform.rotation);
    }
}
