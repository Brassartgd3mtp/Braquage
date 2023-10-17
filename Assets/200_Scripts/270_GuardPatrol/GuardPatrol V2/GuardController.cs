using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    private enum BehaviourName
    {
        Patrol,
        Chase,
        Attack,
        Detection
    }

    private GuardBehaviour currentBehaviour;
    private GuardDetection detectionScript;

    private void ChangeBehaviour(BehaviourName behaviour)
    {
        switch (behaviour)
        {
            case BehaviourName.Patrol:
                currentBehaviour = new GuardPatrolV2();
                break;
            case BehaviourName.Chase:
                currentBehaviour = new GuardChase();
                break;
            case BehaviourName.Attack:
                currentBehaviour = new GuardAttack();
                break;
            case BehaviourName.Detection:
                currentBehaviour = new GuardDetection();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        BehaviourName nextBehaviour;
        if (CheckTransition(out nextBehaviour))
        {
            ChangeBehaviour(nextBehaviour);
        }

        currentBehaviour.Execute();
    }

    private bool CheckTransition(out BehaviourName outBehaviour)
    {
        // Ajoutez votre logique de transition ici
        //if (detect)
        //{
        //    outBehaviour = BehaviourName.Chase;
        //    return true;
        //}

        outBehaviour = BehaviourName.Patrol;
        return false;
    }
}
