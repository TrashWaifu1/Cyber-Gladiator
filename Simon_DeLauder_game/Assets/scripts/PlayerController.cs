using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D weaponJointRB;
    public GameObject weaponJoint;
    public Camera cam;
    public Rigidbody2D rb;
    public float speed = 5;
    public float jumpHight = 5;
    public int Health = 3;
    public int maxHealth = 3;
    public Vector2 velocity;

    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - weaponJointRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        weaponJointRB.rotation = angle;
        //weaponJoint.transform.SetPositionAndRotation.

        velocity = rb.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKey(KeyCode.Space) && Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.52f), Vector2.down, 0.01f))
            velocity.y = jumpHight;

        rb.velocity = velocity;

        if (Health == 0)
        {
            transform.position = new Vector2(0, 0);
            Health = maxHealth;
        }
    }
}
