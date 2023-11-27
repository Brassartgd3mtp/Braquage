using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCache : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Vérifiez si le joueur quitte la cachette
        PlayerCache playerCache = other.GetComponent<PlayerCache>();

        if (playerCache != null)
        {
            // Le joueur a quitté la cachette, met à jour le booléen isHidden dans le script du joueur
            playerCache.SetHiddenState(false);
        }
    }
}
