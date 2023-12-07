using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string MainMenuScene; // Nom des sc�nes � charger
    public string LevelScene;

    public void LoadMainMenuScene()
    {
        // Charge la nouvelle sc�ne en utilisant le nom de la sc�ne
        SceneManager.LoadScene(MainMenuScene);
    }
    public void LoadLevelScene()
    {
        // Charge la nouvelle sc�ne en utilisant le nom de la sc�ne
        SceneManager.LoadScene(LevelScene);
    }
}
