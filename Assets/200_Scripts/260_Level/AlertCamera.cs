using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCamera : MonoBehaviour
{
    public AlertTimer _alertTimer; // Référence vers le script du timer
    public GameObject _alertActivate; // Référence vers le GameObject à activer/désactiver


    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le tag du collider qui entre est "Player"
        if (other.CompareTag("Player"))
        {
            if (_alertActivate != null)
            {
                _alertActivate.SetActive(true);
            }

            // Démarre le timer lorsque le joueur entre dans la zone
            _alertTimer._isTimerRunning = true;
        }
    }
}
