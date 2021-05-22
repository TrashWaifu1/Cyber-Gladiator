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

    public void guide()
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

    public void colosseum()
    {
        SceneManager.LoadScene(4);
    }

    public void street()
    {
        SceneManager.LoadScene(5);
    }

    public void forest()
    {
        SceneManager.LoadScene(6);
    }

    public void debug()
    {
        SceneManager.LoadScene(7);
    }
    #endregion
}
