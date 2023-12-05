using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Nom de la scène à charger

    public void LoadScene()
    {
        // Charge la nouvelle scène en utilisant le nom de la scène
        SceneManager.LoadScene(sceneToLoad);
    }
}
