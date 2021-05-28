using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5000;
    public float speed;
    public int damage = 50;
    public Rigidbody2D rb;

    Stopwatch lifetimeTimer = Stopwatch.StartNew();

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (lifetimeTimer.ElapsedMilliseconds > lifetime)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

        if (collision.gameObject.tag != "Player")
        Destroy(gameObject);
    }

    

}
