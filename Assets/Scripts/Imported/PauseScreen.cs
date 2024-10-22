using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [Header("Essentials")]
    public GameObject pauseMenu;
    public string titleScreen;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1.0f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(currentScene);
    }

    public void Exit()
    {
        SceneManager.LoadScene(titleScreen);
    }
}
