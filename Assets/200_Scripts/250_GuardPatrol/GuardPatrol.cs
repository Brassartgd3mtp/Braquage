using UnityEngine;
using UnityEngine.AI;
using static GuardController;



public class GuardPatrol : GuardBehaviour
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

    public LayerMask excludedLayers; // D�finissez le layer mask dans l'inspecteur

    public AlertTimer alertTimer; // R�f�rence vers le script du timer
    public GameObject alertActivate; // R�f�rence vers le GameObject � activer/d�sactiver

    private NavMeshAgent agent;

    private Animator myAnimator;
    private bool isWalking = false;
    private bool isRun = false;

    [SerializeField] private float waypointTimer = 1.0f;
    [SerializeField] private float currentTimer = 0.0f;

    [SerializeField] private float minWaypointTimer = 1.0f;
    [SerializeField] private float maxWaypointTimer = 5.0f;

    public ObjectDoorV2 doorToInteract;
    public float doorInteractionDistance = 2.0f; // Ajoute cette ligne


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();

        currentTimer = waypointTimer;

        SetDestination();
    }

    public void ResetDestination()
    {
        SetDestination();
    }

    public override void ApplyBehaviour()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.2f && !DetectPlayer())
        {
            agent.isStopped = false;

            SetNextWaypoint();
        }
    }

    public override BehaviourName CheckTransition()
    {
        if (DetectPlayer())
        {
            // Retourner la valeur de l'enum correspondant au prochain comportement
            return BehaviourName.Chase;
        }

        // Aucune transition sp�cifique depuis la patrouille.
        return BehaviourName.None;
    }

    public void SetDestination()
    {
        agent.isStopped = false;
        agent.speed = normalSpeed;
        agent.SetDestination(waypoints[currentWaypoint].position);
        
        isWalking = true;
        myAnimator.SetBool("Walk", isWalking);
        
        isRun = false;
        myAnimator.SetBool("Run", isRun);
    }

    private void SetNextWaypoint()
    {
            isWalking = false;
            myAnimator.SetBool("Walk", isWalking);

            isRun = false;
            myAnimator.SetBool("Run", isRun);

        // V�rifie si le timer est �coul�
        if (currentTimer <= 0)
        {
            currentWaypoint += patrolDirection;


            if (currentWaypoint >= waypoints.Length || currentWaypoint < 0)
            {
                patrolDirection *= -1;
                currentWaypoint += patrolDirection * 2;
            }

            waypointTimer = Random.Range(minWaypointTimer, maxWaypointTimer);

            CheckForDoorInteraction();

            SetDestination();

            // R�initialise le timer apr�s avoir appel� SetNextWaypoint
            currentTimer = waypointTimer;
        }
        else
        {
            // D�cr�mente le timer si le timer n'est pas encore �coul�
            currentTimer -= Time.deltaTime;
        }
    }
    private void CheckForDoorInteraction()
    {
        // V�rifie s'il y a une porte � proximit�
        if (doorToInteract != null && Vector3.Distance(transform.position, doorToInteract.transform.position) < doorInteractionDistance)
        {
            // V�rifie si la porte est ouverte ou ferm�e
            if (doorToInteract.isOpening)
            {
                doorToInteract.CloseDoor();
            }
            else
            {
                doorToInteract.OpenDoor();
            }
        }
    }
    public bool DetectPlayer()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            // Calculer l'angle pour chaque rayon
            float angle = i * detectionAngle / (numberOfRays - 1) - detectionAngle * 0.5f;
        
            // Calculer la direction du rayon en fonction de l'angle
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
        
            // Dessiner le rayon de d�tection avec une ligne rouge dans l'�diteur
            Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);
        
            RaycastHit hit;
        
            // Lancer un rayon dans la direction actuelle
            if (Physics.Raycast(transform.position, direction, out hit, detectionRadius, ~excludedLayers) && hit.collider.CompareTag(playerTag))
            {
                PlayerCache playerCache = hit.collider.GetComponent<PlayerCache>();
                if (playerCache != null && !playerCache.isHidden)
                {
                    ActivateAlert();
                    return true;
                }
            }
        }
        // Si aucun joueur n'est d�tect�, retourner faux
        return false;
    }

    void ActivateAlert()
    {
        if (alertActivate != null)
        {
            alertActivate.SetActive(true);
        }

        // D�marre le timer lorsque le joueur entre dans la zone
        alertTimer._isTimerRunning = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, detectionAngle * 0.5f, detectionRadius, 0f, 1f);
    }
}