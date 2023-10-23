using System.Collections;
using System.Collections.Generic;
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

}
