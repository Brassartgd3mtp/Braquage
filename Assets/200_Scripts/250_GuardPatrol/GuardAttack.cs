using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static GuardController;

public class GuardAttack : GuardBehaviour
{
    public override float DetectionRadius => detectionRadius;
    public override float DetectionAngle => detectionAngle;
    public override float NumberOfRays => numberOfRays;
    public override string PlayerTag => playerTag;

    public float attackRange = 2f; // Distance d'attaque
    private GameObject[] players;

    public Animator animator;

    public bool attackPlayer = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void ApplyBehaviour()
    {
        // Vérifier si le joueur est à portée d'attaque
        if (DetectPlayer() && Vector3.Distance(transform.position, GetClosestPlayer().transform.position) <= attackRange && !attackPlayer)
        {
            // Attaquer si le joueur est à portée
            Attack();
        }
    }

    public override BehaviourName CheckTransition()
    {
        if (!DetectPlayer() || Vector3.Distance(transform.position, GetClosestPlayer().transform.position) > attackRange)
        {
            Debug.Log("Player out of attack range. Transitioning back to Chase.");
            return BehaviourName.Chase;
        }
        return BehaviourName.None;
    }

    private bool DetectPlayer()
    {
        // Trouver tous les objets avec le tag du joueur
        players = GameObject.FindGameObjectsWithTag("Player");

        // Vérifier s'il y a des joueurs dans la scène
        if (players.Length > 0)
        {
            // Vérifier si le tag du joueur est effectivement "Player"
            return players.Any(player => player != null && player.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < detectionRadius);
        }

        // Aucun joueur détecté
        return false;
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
            // Vérifier si le tag du joueur est effectivement "Player"
            if (player != null && player.CompareTag("Player"))
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
        }

        // Retourner le joueur le plus proche
        return closestPlayer;
    }

    private void Attack()
    {
        // Logique d'attaque ici, peut-être activer une animation, infliger des dégâts, etc.
        Debug.Log("Attacking!");

        // Récupérer le joueur le plus proche
        GameObject closestPlayer = GetClosestPlayer();

        UnitMovement unitMovement = closestPlayer.GetComponent<UnitMovement>();

        if (unitMovement != null)
        {
            unitMovement.Immobilize();
            attackPlayer = true;
        }

        animator.SetBool("Immobilisation", true);
    }
}