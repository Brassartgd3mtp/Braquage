using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    private Animator myAnimator;
    private NavMeshAgent myAgent;

    public bool isWalking = false;
    public bool isDoingAction = false;
    public bool isDowned = false;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isWalking && myAgent.remainingDistance <= myAgent.stoppingDistance)
        {
            // L'agent a atteint sa destination, arrête de marcher
            isWalking = false;
        }
        myAnimator.SetBool("Walk", isWalking);
        myAnimator.SetBool("IsDowned?", isDowned);
    }
}
