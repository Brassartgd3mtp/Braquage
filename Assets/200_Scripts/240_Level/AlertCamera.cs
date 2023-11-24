using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCamera : MonoBehaviour
{
    public AlertTimer _alertTimer; // R�f�rence vers le script du timer
    public GameObject _alertActivate; // R�f�rence vers le GameObject � activer/d�sactiver

    public int numberOfRaycasts = 5;       // Nombre de raycasts dans le c�ne
    public float detectionDistance = 10f;  // Distance maximale pour la d�tection
    public float coneAngle = 45f;          // Angle du c�ne de d�tection

    void Update()
    {
        // Direction du c�ne de d�tection (avant du GameObject)
        Vector3 detectionDirection = transform.forward;

        // Calcul de l'angle entre les raycasts
        float angleBetweenRaycasts = coneAngle / (float)(numberOfRaycasts - 1);

        // Raycasts en c�ne
        for (int i = 0; i < numberOfRaycasts; i++)
        {
            // Calcule la direction du raycast en c�ne
            Quaternion rotation = Quaternion.Euler(0, -(coneAngle / 2) + (i * angleBetweenRaycasts), 0);
            Vector3 rayDirection = rotation * detectionDirection;

            // Raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, detectionDistance) && hit.collider.CompareTag("Player"))
            {
                PlayerCache playerCache = hit.collider.GetComponent<PlayerCache>();
                if (playerCache != null && !playerCache.isHidden)
                {
                    // L'objet est d�tect�, faites quelque chose ici (par exemple, activez une alerte)
                    ActivateAlert();
                }
            }

            // Dessiner des gizmos pour visualiser les raycasts (� des fins de d�bogage)
            Debug.DrawRay(transform.position, rayDirection * detectionDistance, Color.yellow);
        }
    }

    void ActivateAlert()
    {
        // Code pour activer l'alerte (� impl�menter selon vos besoins)
        Debug.Log("Alert activated!");


        if (_alertActivate != null)
        {
            _alertActivate.SetActive(true);
        }

        // D�marre le timer lorsque le joueur entre dans la zone
        _alertTimer._isTimerRunning = true;

    }

    // Dessiner des gizmos pour visualiser le c�ne de d�tection dans l'�diteur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float halfAngle = coneAngle / 2f;
        Quaternion leftRayRotation = Quaternion.Euler(0, -halfAngle, 0);
        Quaternion rightRayRotation = Quaternion.Euler(0, halfAngle, 0);
        Vector3 forward = transform.forward * detectionDistance;

        Gizmos.DrawRay(transform.position, leftRayRotation * forward);
        Gizmos.DrawRay(transform.position, rightRayRotation * forward);
        Gizmos.DrawLine(transform.position + leftRayRotation * forward, transform.position + rightRayRotation * forward);
    }
}