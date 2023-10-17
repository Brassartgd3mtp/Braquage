using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    public GuardPatrol guardPatrol;
    public GuardPlayerDetection playerDetection;

    // Param�tres de d�tection d'origine
    public float originDetectionAngle = 160f;
    public float originDetectionRadius = 7.5f;
    public int originNumbersOfRays = 15;

    // Nouveaux param�tres de d�tection lorsque le joueur est d�tect�
    public float newDetectionAngle = 360f;
    public float newDetectionRadius = 9f;
    public int newNumbersOfRays = 30;

    public AlertTimer _alertTimer; // R�f�rence vers le script du timer
    public GameObject _alertActivate; // R�f�rence vers le GameObject � activer/d�sactiver



    void Start()
    {
        guardPatrol = GetComponent<GuardPatrol>();
        playerDetection = GetComponent<GuardPlayerDetection>();
        //guardPatrol.SetDestination();
    }
    /*
    private enum BehaviourName
    {
        Patrol, 
        Chase,
        Attack
    }
    private Behaviour currentBehavuiour = guardPatrol;
    private void ChangeBehaviour(BehaviourName _behaviour)
    {
        switch (_behaviour)
        {
            case BehaviourName.Patrol:
                currentBehavuiour = guardPatrol;// h�rite de Behaviour
                break;
            case BehaviourName.Chase:
                currentBehavuiour = guardChase;// h�rite de Behaviour
                break;
            case BehaviourName.Attack:
                currentBehavuiour = guardAttack; // h�rite de Behaviour
                break;
            default:
                break;
        }
    }
    
    private bool CheckTransition(out BehaviourName _outBehaviour)
    {
        if(detect)
        {
            _outBehaviour = BehaviourName.Chase;
            return true; 
        }
    }
    */

    void Update()
    {
        if (playerDetection.DetectPlayer())
        {
            // Arr�ter la patrouille et d�placer le garde vers le joueur
            guardPatrol.StopPatrol();
            guardPatrol.MoveTowardsPlayer(playerDetection.FindClosestPlayer());

            // Modifier les param�tres de d�tection du joueur
            playerDetection.detectionAngle = newDetectionAngle;
            playerDetection.detectionRadius = newDetectionRadius;
            playerDetection.numberOfRays = newNumbersOfRays;

            if (_alertActivate != null)
            {
                _alertActivate.SetActive(true);
            }

            // D�marre le timer lorsque le joueur entre dans la zone
            _alertTimer._isTimerRunning = true;


        }
        else
        {
            // Si le garde a atteint le waypoint et n'a pas de nouvelle destination
            if (guardPatrol.Agent.remainingDistance < 0.1f && !guardPatrol.Agent.pathPending)
            {
                // Choisir le prochain waypoint
                guardPatrol.SetNextWaypoint();
            }

            // R�tablir les param�tres de d�tection d'origine
            playerDetection.detectionAngle = originDetectionAngle;
            playerDetection.detectionRadius = originDetectionRadius;
            playerDetection.numberOfRays = originNumbersOfRays;

        }
    }
}
