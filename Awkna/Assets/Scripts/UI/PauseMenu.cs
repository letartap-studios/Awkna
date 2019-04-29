using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject gameoverMenuUI;

    private void Awake()
    {
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (PlayerStats.Instance.Health <= 0)
        {
            GameOver();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameoverMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void GameOver()
    {
        gameoverMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu...");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameoverMenuUI.SetActive(false);
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }


}
