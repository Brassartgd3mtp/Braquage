using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardDetection : GuardBehaviour
{
    private float detectionRange = 10f; // Ajustez la port�e de d�tection selon vos besoins
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
            // Vous pouvez �galement d�finir la destination du NavMeshAgent ici
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
        // Utilisez un raycast pour d�tecter un joueur ou un objet devant le garde
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Le joueur est d�tect�
            }
        }

        return false;
    }
}

