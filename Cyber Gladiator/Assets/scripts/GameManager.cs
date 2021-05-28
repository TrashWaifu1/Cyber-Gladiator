using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int roundMod = 10;
    public GameObject player;
    public GameObject spawnManager;
    public GameObject pauesScreen;
    public GameObject levelOverScreen;
    public SpawnManager SpawnManager;
    public UnityEngine.UI.Button[] levelButtons;
    public TextMeshProUGUI[] highScoreTxt;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI roundTextEndGame;

    public bool pause;

    private void Awake()
    {
        for (int i = 0; i < highScoreTxt.Length; i++)
            highScoreTxt[i].SetText("High Score\n" + PlayerPrefs.GetInt("HighScore-Level" + (i + 4)).ToString());

        for (int i = 1; i <= levelButtons.Length; i++)
                levelButtons[i - 1].interactable = PlayerPrefs.GetInt("HighScore-Level" + (i + 3)) >= (1 * roundMod);  
    }

    void Update()
    {
        healthText.SetText("Health: " + player.GetComponent<PlayerController>().health);
        roundText.SetText("Round: " + SpawnManager.roundNum);
        roundTextEndGame.SetText("Round " + SpawnManager.roundNum);

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
        Score();
        

        levelOverScreen.SetActive(true);
        PauseCheck();
    }

    void Score()
    {
        string valueName = "HighScore-Level" + SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt(valueName) < SpawnManager.roundNum)
            PlayerPrefs.SetInt(valueName, SpawnManager.roundNum);
    }

    #region Buttons
    public void gotoMainMenuInGame()
    {
        Score();
        PauseCheck();
        SceneManager.LoadScene(0);
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

    public void Reload()
    {
        PauseCheck();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void clearData()
    {
        for (int i = 0; i < 4; i++)
            PlayerPrefs.SetInt("HighScore-Level" + (i + 4), 0);
    }
    #endregion
}
