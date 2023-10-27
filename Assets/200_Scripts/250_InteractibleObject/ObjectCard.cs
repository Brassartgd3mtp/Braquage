using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCard : InteractibleObjectV2
{
    public bool unlocksRedCard = false;
    public bool unlocksBlueCard = false;
    public bool unlocksGreenCard = false;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant en collision est le joueur
        PlayerRole accesCard = other.GetComponent<PlayerRole>();
        if (accesCard != null)
        {
            // Définis les booléens du joueur en fonction des paramètres de la carte
            if (unlocksRedCard)
            {
                accesCard.redCard = true;
            }

            if (unlocksBlueCard)
            {
                accesCard.blueCard = true;
            }

            if (unlocksGreenCard)
            {
                accesCard.greenCard = true;
            }

            // Active ou désactive l'objet carte après utilisation
            gameObject.SetActive(false);
        }
    }
}
