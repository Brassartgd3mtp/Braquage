using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardDetection : GuardBehaviour
{
    private float detectionRange = 10f; // Ajustez la portée de détection selon vos besoins
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public override void Execute()
    {
        if (CheckDetection())
        {
            // Logique de transition vers Chase, par exemple
            Debug.Log("Player detected! Switching to Chase.");
            // Vous pouvez également définir la destination du NavMeshAgent ici
            navMeshAgent.destination = playerTransform.position;
        }
        else
        {
            // Logique de patrouille ou autre
            Debug.Log("No player detected. Patrolling.");
        }
    }

    private bool CheckDetection()
    {
        // Utilisez un raycast pour détecter un joueur ou un objet devant le garde
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Le joueur est détecté
            }
        }

        return false;
    }
}

