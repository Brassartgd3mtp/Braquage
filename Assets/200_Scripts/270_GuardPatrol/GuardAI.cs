using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    public GuardPatrol guardPatrol;
    public GuardPlayerDetection playerDetection;

    // Paramètres de détection d'origine
    public float originDetectionAngle = 160f;
    public float originDetectionRadius = 7.5f;
    public int originNumbersOfRays = 15;

    // Nouveaux paramètres de détection lorsque le joueur est détecté
    public float newDetectionAngle = 360f;
    public float newDetectionRadius = 9f;
    public int newNumbersOfRays = 30;

    public AlertTimer _alertTimer; // Référence vers le script du timer
    public GameObject _alertActivate; // Référence vers le GameObject à activer/désactiver



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
                currentBehavuiour = guardPatrol;// hérite de Behaviour
                break;
            case BehaviourName.Chase:
                currentBehavuiour = guardChase;// hérite de Behaviour
                break;
            case BehaviourName.Attack:
                currentBehavuiour = guardAttack; // hérite de Behaviour
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
            // Arrêter la patrouille et déplacer le garde vers le joueur
            guardPatrol.StopPatrol();
            guardPatrol.MoveTowardsPlayer(playerDetection.FindClosestPlayer());

            // Modifier les paramètres de détection du joueur
            playerDetection.detectionAngle = newDetectionAngle;
            playerDetection.detectionRadius = newDetectionRadius;
            playerDetection.numberOfRays = newNumbersOfRays;

            if (_alertActivate != null)
            {
                _alertActivate.SetActive(true);
            }

            // Démarre le timer lorsque le joueur entre dans la zone
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

            // Rétablir les paramètres de détection d'origine
            playerDetection.detectionAngle = originDetectionAngle;
            playerDetection.detectionRadius = originDetectionRadius;
            playerDetection.numberOfRays = originNumbersOfRays;

        }
    }
}
