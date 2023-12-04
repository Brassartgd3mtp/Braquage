using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    //Cr�er les r�les pour les personnages(Script r�le)
    //Technicien - il a la perceuse pour ouvrir le coffre
    //Crocheteur - Crochette plus vite
    //Transporteur - Porte des charges lourde
    //Avec leurs stats
    //Efficacit� de crochetage(Float)
    //Porte un objet lourd oui/non, influe la vitesse de mouvement sauf si Transporteur
    //Vitesse de mouvement(Script Unit Movement)
    //A la per�euse(Bool)

    public bool _technician = true; //Transporte la perceuse pour ouvrir les coffres lourds
    public bool _lockPicker; // Permet de crocheter les portes classique plus vite
    public bool _transporter; //N'a pas de malus de d�placement pendant le transport d'objets lourds

    public float _pickingMultiplier = 1.0f;

    public bool redCard = false;
    public bool blueCard = false;
    public bool greenCard = false;

    public TextMeshProUGUI bountyText; // R�f�rence � l'objet TextMeshPro

    private void Start()
    {
        if (bountyText == null)
        {
            Debug.LogError("La r�f�rence � l'objet TextMeshPro n'est pas d�finie. Assure-toi de l'assigner dans l'inspecteur Unity.");
        }
        else
        {
            BountyManager.Instance.bountyTexts.Add(bountyText);
        }
    }


}
