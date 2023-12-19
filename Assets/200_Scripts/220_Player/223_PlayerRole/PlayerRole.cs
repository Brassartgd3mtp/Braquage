using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    //Créer les rôles pour les personnages(Script rôle)
    //Technicien - il a la perceuse pour ouvrir le coffre
    //Crocheteur - Crochette plus vite
    //Transporteur - Porte des charges lourde
    //Avec leurs stats
    //Efficacité de crochetage(Float)
    //Porte un objet lourd oui/non, influe la vitesse de mouvement sauf si Transporteur
    //Vitesse de mouvement(Script Unit Movement)
    //A la perçeuse(Bool)

    public bool technician = true; //Transporte la perceuse pour ouvrir les coffres lourds
    public bool lockPicker; // Permet de crocheter les portes classique plus vite
    public bool transporter; //N'a pas de malus de déplacement pendant le transport d'objets lourds

    public float _pickingMultiplier = 1.0f;

    private bool takeGoal = false;
    private bool redCard = false;
    public bool blueCard = false;
    public bool greenCard = false;


    public GameObject redCardUI;
    public GameObject takeGoalUI;

    

    public bool RedCard
    {
        get { return redCard; }
        set
        {
            redCard = value;
            redCardUI.SetActive(redCard);
            PlaySFX(13);
        }
    }

    public bool TakeGoal
    {
        get { return takeGoal; }
        set
        {
            takeGoal = value;
            takeGoalUI.SetActive(takeGoal);
        }
    }

    private void PlaySFX(int soundID)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(soundID, audioSource);
    }
}