using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoDelay : MonoBehaviour
{
    public GameObject NewDisplay;
    public GameObject OldDisplay;

    private bool AlreadyDone = false;
    public float Timer = 4;
    public bool Condition = false;
    // Ce script permet d'activer un gameObject de tuto apr�s que le gameObject de tuto d'avant
    // se soit fait pass� par le joueur avec un latence entre eux.
    private void Start()
    {
        NewDisplay.SetActive(false);
    }
    void Update()
    {
        if (!Condition)
        {
            //La Condition doit �tre activ�e si le OldDisplay n'est pas actif d�s le d�but du jeu.
            if (!OldDisplay.activeSelf)
            {
                if (Timer <= 0)
                {
                    // AlreadyDone permet d'activer le GameObject de Tuto une seule fois
                    if (!AlreadyDone)
                    {
                        NewDisplay.gameObject.SetActive(true);
                        AlreadyDone = true;
                    }
                }
                else
                {
                    Timer -= Time.deltaTime;
                }
            }
        }
        else
        {
            if(OldDisplay.activeSelf)
            {
                Condition = false;
            }
        }
    }
}
