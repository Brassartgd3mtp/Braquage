using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyImmobilize : MonoBehaviour
{
    private void Update()
    {
        // Appelle la méthode pour vérifier le nombre de joueurs immobilisés au début.
        VerifierJoueursImmobilises();
    }

    private void VerifierJoueursImmobilises()
    {
        // Obtient tous les enfants du GameObject actuel.
        foreach (Transform enfant in transform)
        {
            // Vérifie si le joueur a le booléen "immobilize" en true.
            if (enfant.GetComponent<UnitMovement>() != null && enfant.GetComponent<UnitMovement>().immobilize)
            {
                // Fais quelque chose avec le joueur immobilisé (par exemple, imprime son nom).
                Debug.Log(enfant.name + " est immobilisé !");
            }
        }
    }
}
