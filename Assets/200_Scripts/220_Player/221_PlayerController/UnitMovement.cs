// Ce script gère le mouvement d'une unité en utilisant NavMeshAgent dans un projet Unity.
// Il permet à l'unité de se déplacer vers un point du sol lorsqu'un clic droit de la souris est effectué.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    public LayerMask interactible;

    public float speed = 3.5f;
    public bool immobilize = false;

    private AnimationController animationController;

    public static List<UnitMovement> unitMovementList = new List<UnitMovement>();

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = speed;
        animationController = GetComponent<AnimationController>();

        enabled = false;

        unitMovementList.Add(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            // Effectue un raycast depuis la position de la souris vers le sol
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                // Définit la destination de l'agent NavMesh vers le point touché par le raycast si le layer mask est ground
                myAgent.SetDestination(hit.point);

                animationController.isWalking = true;
            }
        }
    }

    public void Immobilize()
    {
        myAgent.speed = 0f;
        animationController.isDowned = true;
        immobilize = true;
        for (int i = 0; i < unitMovementList.Count; i++)
        {
            if (!unitMovementList[i].immobilize)
            {
                return;
            }
        }
        Defeat.Instance.DefeatGame();
    }

    public void NoImmobilize()
    {
        myAgent.speed = speed;
        animationController.isDowned = false;
        immobilize = false;
    }
}