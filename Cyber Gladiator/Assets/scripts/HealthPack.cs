using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 25;
    public float lifetime = 10000;
    Stopwatch lifetimeTimer = Stopwatch.StartNew();

    private void Update()
    {
        if (lifetimeTimer.ElapsedMilliseconds > lifetime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().heal(healAmount);
            Destroy(gameObject);
        } 
    }
}
