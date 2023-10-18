using UnityEngine;
using UnityEngine.AI;
using static GuardController;



public class GuardPatrolV2 : GuardBehaviour
{
    public override float DetectionRadius => detectionRadius;
    public override float DetectionAngle => detectionAngle;
    public override float NumberOfRays => numberOfRays;
    public override string PlayerTag => playerTag;

    [Header("Liste Waypoints")]
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private int patrolDirection = 1;

    public float normalSpeed = 3.5f;
    private NavMeshAgent agent;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    public void ResetDestination()
    {
        SetDestination();
    }

    public override void ApplyBehaviour()
    {
        //if (DetectPlayer())
        //{
        //    agent.isStopped = true;
        //}
        // Si le garde atteint le waypoint actuel, détermine le prochain waypoint.
        if (!agent.pathPending && agent.remainingDistance < 0.2f && !DetectPlayer())
        {
            Debug.Log("Changement de waypoint");
            agent.isStopped = false;
            SetNextWaypoint();
        }
    }

    public override BehaviourName CheckTransition()
    {
        if (DetectPlayer())
        {
            Debug.Log("Player detected dans le Checking");

            // Retourner la valeur de l'enum correspondant au prochain comportement
            return BehaviourName.Chase;
        }

        // Aucune transition spécifique depuis la patrouille.
        return BehaviourName.None;
    }

    public void SetDestination()
    {
        agent.isStopped = false;
        agent.speed = normalSpeed;
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    private void SetNextWaypoint()
    {
        Debug.Log("SetNextWaypoint appelée");

        currentWaypoint += patrolDirection;

        if (currentWaypoint >= waypoints.Length || currentWaypoint < 0)
        {
            patrolDirection *= -1;
            currentWaypoint += patrolDirection * 2;
        }

        SetDestination();
    }

    public bool DetectPlayer()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            // Calculer l'angle pour chaque rayon
            float angle = i * detectionAngle / (numberOfRays - 1) - detectionAngle * 0.5f;

            // Calculer la direction du rayon en fonction de l'angle
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Dessiner le rayon de détection avec une ligne rouge dans l'éditeur
            Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);

            RaycastHit hit;

            // Lancer un rayon dans la direction actuelle
            if (Physics.Raycast(transform.position, direction, out hit, detectionRadius) && hit.collider.CompareTag(playerTag))
            {
                // Si le rayon touche un objet portant le tag du joueur, retourner vrai
                return true;
            }
        }
        // Si aucun joueur n'est détecté, retourner faux
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, detectionAngle * 0.5f, detectionRadius, 0f, 1f);
    }
}