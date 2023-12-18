using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivePause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject PauseDisplay;
    public GameObject InputDisplay;
    private bool InputActive = false;

    private void Start()
    {
        PauseDisplay.SetActive(false);
        ResumeGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (InputActive)
                {
                    InputDisplay.SetActive(false);
                    InputActive = false;
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        PauseDisplay.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PauseDisplay.gameObject.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        PauseDisplay.gameObject.SetActive(false);
    }
    public void InputButton()
    {
        InputDisplay.gameObject.SetActive(true);
        InputActive = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
