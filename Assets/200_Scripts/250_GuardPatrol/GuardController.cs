using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class GuardController : MonoBehaviour
{
    public enum BehaviourName
    {
        Patrol,
        Chase,
        Attack,
        None
    }

    private GuardBehaviour currentBehaviour;

    private GuardPatrol guardPatrol;
    private GuardChase guardChase;
    private GuardAttack guardAttack;

    public bool guardDead = false;


    private void Start()
    {
        guardPatrol = GetComponent<GuardPatrol>();
        guardChase = GetComponent<GuardChase>();
        guardAttack = GetComponent<GuardAttack>();
        currentBehaviour = guardPatrol;

    }
    private void Update()
    {
        currentBehaviour.ApplyBehaviour();

        // Vérifier la transition
        BehaviourName nextBehaviour = currentBehaviour.CheckTransition();

        // Changer de comportement si nécessaire
        if (nextBehaviour != BehaviourName.None)
        {
            ChangeBehaviour(nextBehaviour);
        }
        else
        {
            //Debug.LogError("currentBehaviour is null. Ensure it is properly initialized.");
        }
    }

    public void ChangeBehaviour(BehaviourName behaviour)
    {
        currentBehaviour.enabled = false;

        switch (behaviour)
        {
            case BehaviourName.Patrol:
                currentBehaviour = guardPatrol;
                guardPatrol.SetDestination();
                break;
            case BehaviourName.Chase:
                currentBehaviour = guardChase;
                break;
            case BehaviourName.Attack:
                currentBehaviour = guardAttack;
                break;
            default:
                break;
        }
        // Activer le nouveau script
        currentBehaviour.enabled = true;

        Debug.Log($"Switching to {behaviour} state.");

    }    
}