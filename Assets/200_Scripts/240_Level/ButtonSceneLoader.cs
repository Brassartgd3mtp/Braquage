using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Nom de la sc�ne � charger

    public void LoadScene()
    {
        // Charge la nouvelle sc�ne en utilisant le nom de la sc�ne
        SceneManager.LoadScene(sceneToLoad);
    }
}
