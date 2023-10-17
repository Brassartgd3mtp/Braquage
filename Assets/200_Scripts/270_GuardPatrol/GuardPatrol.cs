using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour
{
    // Waypoints pour la patrouille du garde
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private int patrolDirection = 1;

    // Vitesse normale du garde et vitesse lorsqu'il d�tecte un joueur
    public float normalSpeed = 3.5f;
    public float detectionTrueSpeed = 6f;

    // Distance minimale � maintenir entre le garde et le joueur
    public float minDistanceToPlayer = 2f;

    public NavMeshAgent Agent;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        // Initialiser la patrouille en d�finissant la premi�re destination
        SetDestination();
    }


    // Arr�ter la patrouille du garde
    public void StopPatrol()
    {
        Agent.isStopped = true;
    }


    // D�placer le garde vers la position du joueur
    public void MoveTowardsPlayer(Vector3 playerPosition)
    {
        Agent.isStopped = false;

        // Calculer la distance entre le garde et le joueur
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            // Si le garde �tait arr�t�, lui donner la vitesse de d�tection
            if (Agent.speed == 0f)
            {
                Agent.speed = detectionTrueSpeed; // D�finir la vitesse de d�tection
            }

            // Sinon, d�finir la destination du garde � la position du joueur
            else
            {
                Agent.SetDestination(playerPosition);

            }
        }
        else
        {
            // Si le garde est assez proche du joueur, arr�ter le garde
            Agent.speed = 0f;

            //D'autres actions possible � ajouter ici, le combat par exemple
        }
    }

    // D�finir la destination du garde
    public void SetDestination()
    {
        Agent.isStopped = false;

        Agent.speed = normalSpeed; // Assure que la vitesse est r�initialis�e � la vitesse normale

        // D�finir la destination du garde au waypoint actuel
        Agent.SetDestination(waypoints[currentWaypoint].position);
    }

    // Changer le waypoint suivant pour la patrouille
    public void SetNextWaypoint()
    {
        // Avancer vers le prochain waypoint dans la direction de la patrouille
        currentWaypoint += patrolDirection;

        // Inverser la direction si le garde atteint le dernier ou le premier waypoint
        if (currentWaypoint >= waypoints.Length || currentWaypoint < 0)
        {
            patrolDirection *= -1;
            currentWaypoint += patrolDirection * 2;
        }

        SetDestination();
    }
}

