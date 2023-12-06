// Ce script gère le mouvement d'une unité en utilisant NavMeshAgent dans un projet Unity.
// Il permet à l'unité de se déplacer vers un point du sol lorsqu'un clic droit de la souris est effectué.

using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

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

    [SerializeField] private GameObject clickMarkerPrefab;
    [SerializeField] private Transform visualObjectsParent;
    [SerializeField] private LineRenderer myLineRenderer;

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<AnimationController>();
        myAgent.speed = speed;

        myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.startWidth = 0.15f;
        myLineRenderer.endWidth = 0.15f;
        myLineRenderer.positionCount = 0;

        unitMovementList.Add(this);


        enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClickToMove();
        }

        if (myAgent.remainingDistance <= myAgent.stoppingDistance)
        {
            clickMarkerPrefab.transform.SetParent(transform);
            clickMarkerPrefab.SetActive(false);
        }
        else if (myAgent.hasPath)
        {
            DrawPatch();
        }
    }

    void ClickToMove()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        // Effectue un raycast depuis la position de la souris vers le sol
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            SetDestination(hit.point);
        }
    }

    void SetDestination(Vector3 target)
    {
        animationController.isWalking = true;
        clickMarkerPrefab.transform.SetParent(visualObjectsParent);
        clickMarkerPrefab.SetActive(true);
        clickMarkerPrefab.transform.position = target;
        myAgent.SetDestination(target);
    }

    void DrawPatch()
    {
        myLineRenderer.positionCount = myAgent.path.corners.Length;
        myLineRenderer.SetPosition(0, transform.position);

        if (myAgent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < myAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(myAgent.path.corners[i].x, myAgent.path.corners[i].y, myAgent.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPosition);
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