using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove2 : MonoBehaviour
{
    private NavMeshAgent agent; // Composant NavMeshAgent pour gérer le déplacement
    private Camera mainCamera; // Caméra principale pour détecter les clics

    [SerializeField] private float speed = 5f; // Vitesse de déplacement

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Définir la vitesse NavMeshAgent
        agent.speed = speed;

        // Si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            // Vérifier si le personnage est sélectionné
            if (IsSelected())
            {
                // Lancer un rayon depuis la position de la souris sur l'écran
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Vérifier s'il y a une collision avec le terrain
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
                {
                    // Définir la destination du NavMeshAgent sur le point d'impact
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    private bool IsSelected()
    {
        // Vérifier si le personnage a le tag "Player" (vous pouvez ajuster cela en fonction de vos besoins)
        return gameObject.CompareTag("Player");
    }
}
