using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InputGO;
    private void Start()
    {
        InputGO.gameObject.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("CoreScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void InputDisplay()
    {
        InputGO.gameObject.SetActive(true);
    }
}
