using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCamera : MonoBehaviour
{
    public AlertTimer _alertTimer; // R�f�rence vers le script du timer
    public GameObject _alertActivate; // R�f�rence vers le GameObject � activer/d�sactiver


    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le tag du collider qui entre est "Player"
        if (other.CompareTag("Player"))
        {
            if (_alertActivate != null)
            {
                _alertActivate.SetActive(true);
            }

            // D�marre le timer lorsque le joueur entre dans la zone
            _alertTimer._isTimerRunning = true;
        }
    }
}
