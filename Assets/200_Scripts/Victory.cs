using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // R�f�rence � la fen�tre de victoire (� attacher dans l'inspecteur Unity)
    public GameObject victoryWindow;

    // R�f�rence au script de gestion de la pause
    public ActivePause pauseManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Active la fen�tre de victoire et met le jeu en pause lorsque le joueur entre en contact
            ShowVictoryWindow();
            PauseGame();
        }
    }

    private void ShowVictoryWindow()
    {
        if (victoryWindow != null)
        {
            victoryWindow.SetActive(true);
            // Ajoute ici d'autres actions li�es � l'affichage de la fen�tre de victoire
        }
        else
        {
            Debug.LogError("Victory window is not assigned!");
        }
    }

    private void PauseGame()
    {
        if (pauseManager != null)
        {
            pauseManager.PauseGame();
        }
        else
        {
            Debug.LogError("PauseManager script is not assigned!");
        }
    }
}
