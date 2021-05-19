using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().heal(healAmount);
            Destroy(gameObject);
        } 
    }
}
