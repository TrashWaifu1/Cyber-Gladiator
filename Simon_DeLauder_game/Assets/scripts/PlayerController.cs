using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform weaponJoint;
    public Rigidbody2D rb;
    public float speed = 5;
    public float jumpHight = 5;
    public int Health = 3;
    public int maxHealth = 3;
    public Vector2 velocity;
    public Transform firePoint;
    public int playerHealth;
    public GameObject bullet;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            shoot();

        if (Health == 0)
        {
            transform.position = new Vector2(0, 0);
            Health = maxHealth;
        }

        Vector2 mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDifference.Normalize();
        weaponJoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKey(KeyCode.Space) && Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.52f), Vector2.down, 0.01f))
            velocity.y = jumpHight;

        rb.velocity = velocity;
    }

    void shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }


}
