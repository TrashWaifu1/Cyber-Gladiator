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

    #region Buttons
    public void start()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void guid()
    {
        SceneManager.LoadScene(2);
    }

    public void settings()
    {
        SceneManager.LoadScene(3);
    }

    public void quit()
    {
        Application.Quit();
    }
    #endregion
}
