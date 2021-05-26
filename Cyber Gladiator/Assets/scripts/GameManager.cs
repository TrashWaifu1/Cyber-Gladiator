﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;
    public GameObject pauesScreen;
    public GameObject levelOverScreen;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI roundTextEndGame;

    public bool pause;

    void Update()
    {
        healthText.SetText("Health: " + player.GetComponent<PlayerController>().health);
        roundText.SetText("Round: " + spawnManager.GetComponent<SpawnManager>().roundNum);
        roundTextEndGame.SetText("Round " + spawnManager.GetComponent<SpawnManager>().roundNum);

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseCheck();    
    }

    void PauseCheck()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            if (!levelOverScreen.activeInHierarchy)
            pauesScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauesScreen.SetActive(false);
        }
    }

    public void Dead()
    {
        levelOverScreen.SetActive(true);
        PauseCheck();

        PlayerPrefs.SetInt("PlayerRoundCount", spawnManager.GetComponent<SpawnManager>().roundNum);
    }

    #region Buttons
    public void gotoMainMenuInGame()
    {
        SceneManager.LoadScene(0);
        PauseCheck();
    }

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
