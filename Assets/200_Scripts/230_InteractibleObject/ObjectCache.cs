using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCache : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // V�rifiez si le joueur quitte la cachette
        PlayerCache playerCache = other.GetComponent<PlayerCache>();

        if (playerCache != null)
        {
            // Le joueur a quitt� la cachette, met � jour le bool�en isHidden dans le script du joueur
            playerCache.SetHiddenState(false);
        }
    }
}
