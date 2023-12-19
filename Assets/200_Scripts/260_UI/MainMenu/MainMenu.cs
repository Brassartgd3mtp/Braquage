using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InputGO;
    public GameObject LevelIntro;
    private void Start()
    {
        InputGO.gameObject.SetActive(false);
        LevelIntro.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (LevelIntro)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("CoreScene");
            }
        }
    }
    public void PlayGame()
    {
        LevelIntro.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenInputDisplay()
    {
        InputGO.gameObject.SetActive(true);
    }
    public void CloseInputDisplay()
    {
        InputGO.gameObject.SetActive(false);
    }
}
