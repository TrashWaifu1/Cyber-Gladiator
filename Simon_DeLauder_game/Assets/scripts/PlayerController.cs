using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float fireRate = 500;
    public float speed = 5;
    public float jumpHight = 5;
    public int health = 100;
    public int maxHealth = 100;
    public Transform weaponJoint;
    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject sword;
    public TrailRenderer trail;
    public readonly List<float> rotationSamples = new List<float>();

    Stopwatch fireTimer = Stopwatch.StartNew();
    float RotationAverage = 0;
    Quaternion LastRotation = new Quaternion();
    Vector2 velocity;
    bool weaponSlot;

    private void Update()
    {
        UnityEngine.Debug.Log(health);

        switch (weaponSlot)
        {
            case (false):
                if (Input.GetMouseButton(0) && fireTimer.ElapsedMilliseconds > fireRate)
                {
                    shoot();
                    fireTimer.Restart();
                }
                sword.SetActive(false);
                break;
            case (true):
                sword.SetActive(true);

                      rotationSamples.Add((weaponJoint.transform.rotation * Quaternion.Inverse(LastRotation)).eulerAngles.z * Time.deltaTime);
                LastRotation = weaponJoint.transform.rotation;

                if (rotationSamples.Count > 100)
                    rotationSamples.RemoveAt(0);

                RotationAverage = Mathf.Abs(rotationSamples.Sum() / rotationSamples.Count);

                trail.startColor = new Color(0, .8f, 1, Mathf.Clamp(RotationAverage / 1.5f, 0, 1));
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta != new Vector2(0, 0))
            weaponSlot = !weaponSlot;

        

        Vector2 mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDifference.Normalize();
        weaponJoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        transform.position = new Vector2(0, 0);
        health = maxHealth;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.otherCollider.gameObject.name == "Sword")
            collision.gameObject.GetComponent<EnemyController>().TakeDamage((int) (RotationAverage * 50));
    }
}
