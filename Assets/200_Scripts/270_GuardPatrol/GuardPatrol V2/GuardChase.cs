using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static GuardController;

public class GuardChase : GuardBehaviour
{
    // Propriétés d'accès aux paramètres de détection du GuardBehaviour
    public override float DetectionRadius => detectionRadius;
    public override float DetectionAngle => detectionAngle;
    public override float NumberOfRays => numberOfRays;
    public override string PlayerTag => playerTag;


    // Vitesse de poursuite
    public float chaseSpeed = 5f;

    // Tableau pour stocker les joueurs détectés
    private GameObject[] players;

    // Composant NavMeshAgent pour la navigation
    private NavMeshAgent agent;

    private void Start()
    {
        // Initialisation du composant NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
    }

    public override void ApplyBehaviour()
    {
        ChasePlayer();
    }

    public override BehaviourName CheckTransition()
    {
        if (!DetectPlayer())
        {
            Debug.Log("Player out of sight. Transitioning back to Patrol.");
            return BehaviourName.Patrol;
        }

        return BehaviourName.None;
    }

    private void ChasePlayer()
    {
        // Poursuivre le joueur détecté
        if (players != null && players.Length > 0)
        {
            agent.isStopped = false;
            agent.speed = chaseSpeed;

            // Trouver le joueur le plus proche
            GameObject closestPlayer = GetClosestPlayer();

            if (closestPlayer != null)
            {
                // Définir la destination du NavMeshAgent sur la position du joueur le plus proche
                agent.SetDestination(closestPlayer.transform.position);
            }
        }
    }

    private GameObject GetClosestPlayer()
    {
        // Initialiser les variables pour stocker le joueur le plus proche
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        // Parcourir tous les joueurs détectés
        foreach (GameObject player in players)
        {
            // Calculer la distance entre le garde et le joueur actuel
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Mettre à jour le joueur le plus proche si la distance actuelle est plus petite
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        // Retourner le joueur le plus proche
        return closestPlayer;
    }

    private bool DetectPlayer()
    {
        // Trouver tous les objets avec le tag du joueur
        players = GameObject.FindGameObjectsWithTag("Player");

        // Vérifier s'il y a des joueurs dans la scène
        if (players.Length > 0)
        {
            // Logique de détection du joueur (peut être la même que dans GuardPatrolV2)
            // Exemple simplifié : retourne vrai si au moins un joueur est à portée de vue
            return players.Any(player => Vector3.Distance(transform.position, player.transform.position) < detectionRadius);
        }

        // Aucun joueur détecté
        return false;
    }
}
