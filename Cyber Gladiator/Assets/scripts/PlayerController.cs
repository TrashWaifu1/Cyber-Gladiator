using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerController : MonoBehaviour
{
    public float fireRate = 500;
    public float speed = 5;
    public float jumpHeight = 5;
    public int health = 100;
    public int maxHealth = 100;
    public Transform weaponJoint;
    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject sword;
    public GameManager gm;
    public AudioSource audioSource;
    public TrailRenderer trail;
    public readonly List<float> rotationSamples = new List<float>();
    public float RotationAverage = 0;
    public float swordDamageMod = .25f;
    public float swordSensitivity = 40;
    public ParticleSystem boom;
    public SpriteRenderer SR;
    public AudioClip[] sounds;

    Stopwatch fireTimer = Stopwatch.StartNew();
    float LastRotation = 0;
    Vector2 velocity;
    bool weaponSlot;

    private void Update()
    {
        if (!gm.pause)
        {
            switch (weaponSlot)
            {
                case (false):
                    RotationAverage = 0;
                    if (Input.GetMouseButton(0) && fireTimer.ElapsedMilliseconds > fireRate)
                    {
                        shoot();
                        fireTimer.Restart();
                    }
                    sword.SetActive(false);
                    break;
                case (true):
                    sword.SetActive(true);

                    float rotation = weaponJoint.transform.rotation.eulerAngles.z;
                    float distance = Mathf.Abs(rotation % 360 - LastRotation % 360);
                    rotationSamples.Add(Mathf.Min(distance, 360 - distance) / Time.deltaTime);
                    LastRotation = rotation;

                    if (rotationSamples.Count > 100)
                        rotationSamples.RemoveAt(0);

                    RotationAverage = rotationSamples.Sum() / rotationSamples.Count * swordSensitivity;

                    trail.startColor = new Color(0, .8f, 1, Mathf.Clamp(RotationAverage / 1.5f, 0, 1));
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta != new Vector2(0, 0))
                weaponSlot = !weaponSlot;

            Vector2 mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            mouseDifference.Normalize();
            weaponJoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg);
        }
    }

    public void TakeDamage(int damage)
    {
         health -= damage;
        health = Mathf.Clamp(health, 0, 100);

        if (health <= 0)
        {
            SR.enabled = false;
            boom.Play();
            audioSource.PlayOneShot(sounds[0]);
            gm.Dead();
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKey(KeyCode.Space) && Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.52f), Vector2.down, 0.01f))
            velocity.y = jumpHeight;

        rb.velocity = velocity;
    }

    void shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(sounds[2]);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.otherCollider.gameObject.name == "Sword")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage((int)(RotationAverage * swordDamageMod));
            audioSource.PlayOneShot(sounds[1]);
        }
            
    }

    public void heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
}
