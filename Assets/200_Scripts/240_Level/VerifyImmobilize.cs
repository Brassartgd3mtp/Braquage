using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyImmobilize : MonoBehaviour
{
    private void Update()
    {
        // Appelle la m�thode pour v�rifier le nombre de joueurs immobilis�s au d�but.
        VerifierJoueursImmobilises();
    }

    private void VerifierJoueursImmobilises()
    {
        // Obtient tous les enfants du GameObject actuel.
        foreach (Transform enfant in transform)
        {
            // V�rifie si le joueur a le bool�en "immobilize" en true.
            if (enfant.GetComponent<UnitMovement>() != null && enfant.GetComponent<UnitMovement>().immobilize)
            {
                // Fais quelque chose avec le joueur immobilis� (par exemple, imprime son nom).
                Debug.Log(enfant.name + " est immobilis� !");
            }
        }
    }
}
