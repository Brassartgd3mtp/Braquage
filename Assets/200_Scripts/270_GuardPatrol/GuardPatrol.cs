using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); // Liste des points de patrouille
    private int currentWaypointIndex = 0; // Index du point de patrouille actuel
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Assurez-vous d'avoir au moins un waypoint dans la liste
        if (waypoints.Count == 0)
        {
            Debug.LogError("Ajoutez des waypoints au script GuardPatrol.");
        }
        else
        {
            SetDestination(); // Définissez la destination initiale
        }
    }

    void Update()
    {
        // Vérifiez si le garde a atteint sa destination
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            // Passez au waypoint suivant
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            // Définissez la nouvelle destination
            SetDestination();
        }
    }

    void SetDestination()
    {
        // Définissez la destination du garde sur le waypoint actuel
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
