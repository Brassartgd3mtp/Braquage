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
        // V�rifie si l'objet entrant en collision est le joueur
        PlayerRole accesCard = other.GetComponent<PlayerRole>();
        if (accesCard != null)
        {
            // D�finis les bool�ens du joueur en fonction des param�tres de la carte
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

            // Active ou d�sactive l'objet carte apr�s utilisation
            gameObject.SetActive(false);
        }
    }
}
