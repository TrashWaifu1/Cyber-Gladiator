using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSpin : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = Mathf.Clamp(player.GetComponent<PlayerController>().RotationAverage, 0, 1f);
    }
}
