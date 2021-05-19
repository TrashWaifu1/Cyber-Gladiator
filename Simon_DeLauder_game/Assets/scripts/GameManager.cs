using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI roundText;

    void Update()
    {
        healthText.SetText("Health: " + player.GetComponent<PlayerController>().health);
        roundText.SetText("Round: " + spawnManager.GetComponent<SpawnManager>().roundNum);
    }
}
