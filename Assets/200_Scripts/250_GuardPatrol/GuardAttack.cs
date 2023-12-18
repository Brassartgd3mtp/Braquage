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
        // V�rifier si le joueur est � port�e d'attaque
        if (DetectPlayer() && Vector3.Distance(transform.position, GetClosestPlayer().transform.position) <= attackRange && !attackPlayer)
        {
            // Attaquer si le joueur est � port�e
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

        // V�rifier s'il y a des joueurs dans la sc�ne
        if (players.Length > 0)
        {
            // V�rifier si le tag du joueur est effectivement "Player"
            return players.Any(player => player != null && player.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < detectionRadius);
        }

        // Aucun joueur d�tect�
        return false;
    }

    private GameObject GetClosestPlayer()
    {
        // V�rifier si le tableau players est null ou vide
        if (players == null || players.Length == 0)
        {
            return null;
        }

        // Initialiser les variables pour stocker le joueur le plus proche
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        // Parcourir tous les joueurs d�tect�s
        foreach (GameObject player in players)
        {
            // V�rifier si le tag du joueur est effectivement "Player"
            if (player != null && player.CompareTag("Player"))
            {
                // Calculer la distance entre le garde et le joueur actuel
                float distance = Vector3.Distance(transform.position, player.transform.position);

                // Mettre � jour le joueur le plus proche si la distance actuelle est plus petite
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
        // Logique d'attaque ici, peut-�tre activer une animation, infliger des d�g�ts, etc.
        Debug.Log("Attacking!");

        // R�cup�rer le joueur le plus proche
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