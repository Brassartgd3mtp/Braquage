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
    
    public float attackRange = 2f; // Distance d'attaque


    // Vitesse de poursuite
    public float chaseSpeed = 5f;

    // Tableau pour stocker les joueurs détectés
    private GameObject[] players;

    // Composant NavMeshAgent pour la navigation
    private NavMeshAgent agent;

    private Animator myAnimator;
    private bool isWalking = false;
    private bool isRun = false;

    private void Start()
    {
        // Initialisation du composant NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
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
        if (Vector3.Distance(transform.position, GetClosestPlayer().transform.position) <= attackRange)
        {
            Debug.Log("Player in attack range. Transitioning to Attack.");
            return BehaviourName.Attack;
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

            isWalking = false;
            myAnimator.SetBool("Walk", isWalking);

            isRun = true;
            myAnimator.SetBool("Run", isRun);

            // Trouver le joueur le plus proche
            GameObject closestPlayer = GetClosestPlayer();

            if (closestPlayer != null)
            {
                // Vérifier si le joueur actuellement poursuivi est différent du joueur le plus proche
                if (agent.destination != closestPlayer.transform.position)
                {
                    // Définir la destination du NavMeshAgent sur la position du joueur le plus proche
                    agent.SetDestination(closestPlayer.transform.position);
                }
            }
        }
    }

    private GameObject GetClosestPlayer()
    {
        // Vérifier si le tableau players est null ou vide

        if (players == null || players.Length == 0)
        {
            return null;
        }

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
            return players.Any(player => player != null && player.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < detectionRadius);
        }

        // Aucun joueur détecté
        return false;
    }
}