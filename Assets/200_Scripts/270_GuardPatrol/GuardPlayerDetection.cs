using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerDetection : MonoBehaviour
{
    // Rayon, angle et nombre de rayons de d�tection du joueur
    public float detectionRadius = 5f;
    public float detectionAngle = 60f;
    public int numberOfRays = 5;

    public string playerTag = "Player";

    // D�termine si le joueur est d�tect� dans la zone de d�tection
    public bool DetectPlayer()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            // Calculer l'angle pour chaque rayon
            float angle = i * detectionAngle / (numberOfRays - 1) - detectionAngle * 0.5f;

            // Calculer la direction du rayon en fonction de l'angle
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Dessiner le rayon de d�tection avec une ligne rouge dans l'�diteur
            Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);

            RaycastHit hit;

            // Lancer un rayon dans la direction actuelle
            if (Physics.Raycast(transform.position, direction, out hit, detectionRadius) && hit.collider.CompareTag(playerTag))
            {
                // Si le rayon touche un objet portant le tag du joueur, retourner vrai
                return true;
            }
        }
        // Si aucun joueur n'est d�tect�, retourner faux
        return false;
    }

    // Trouver la position du joueur le plus proche
    public Vector3 FindClosestPlayer()
    {
        // Trouver tous les objets avec le tag du joueur
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        if (players.Length > 0)
        {
            GameObject closestPlayer = null;
            float closestDistance = Mathf.Infinity;

            // Parcourir tous les joueurs pour trouver le plus proche
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player;
                }
            }

            // Retourner la position du joueur le plus proche
            return closestPlayer.transform.position;
        }

        // Si aucun joueur n'est trouv�, retourner une position par d�faut
        return Vector3.zero;
    }

    // Dessiner des gizmos pour visualiser la zone de d�tection dans l'�diteur
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Dessiner une sph�re repr�sentant la zone de d�tection
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        // D�finir la matrice pour orienter le c�ne de d�tection dans la direction actuelle
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        // Dessiner un c�ne repr�sentant la zone de d�tection dans l'�diteur
        Gizmos.DrawFrustum(Vector3.zero, detectionAngle * 0.5f, detectionRadius, 0f, 1f);
    }
}
