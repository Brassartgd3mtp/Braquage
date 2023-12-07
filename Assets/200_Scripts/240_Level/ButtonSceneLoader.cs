using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string MainMenuScene; // Nom des scènes à charger
    public string LevelScene;

    public void LoadMainMenuScene()
    {
        // Charge la nouvelle scène en utilisant le nom de la scène
        SceneManager.LoadScene(MainMenuScene);
    }
    public void LoadLevelScene()
    {
        // Charge la nouvelle scène en utilisant le nom de la scène
        SceneManager.LoadScene(LevelScene);
    }
}
