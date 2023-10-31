using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielOneDisplaySkip : MonoBehaviour
{
    public GameObject mainWindow;
    public GameObject continueButton;
    private bool CanNext = false;
    private float Cooldown = 2;


    void Start()
    {
        continueButton.SetActive(false);
    }

    /// <summary>
    /// Pour chaque fenêtre qui s'active pour le tuto, tant que le timer n'est 
    /// pas à 0, le joueur ne peut pas appuyer sur espace pour skipper.
    /// </summary>

    void Update()
    {

        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }
        if (Cooldown <= 0)
        {
            continueButton.gameObject.SetActive(true);
        }
        if (continueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mainWindow.gameObject.SetActive(false);
                continueButton.gameObject.SetActive(false);
            }
        }
    }
}

