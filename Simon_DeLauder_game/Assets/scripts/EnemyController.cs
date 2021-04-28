using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
            velocity.x = -5f;
        }
        else if (isFolowing)
        {
            velocity.x = 5f;
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
}
