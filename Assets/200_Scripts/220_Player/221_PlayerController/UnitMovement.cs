// Ce script gère le mouvement d'une unité en utilisant NavMeshAgent dans un projet Unity.
// Il permet à l'unité de se déplacer vers un point du sol lorsqu'un clic droit de la souris est effectué.

using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask _ground;
    public float speed = 3.5f; // Ajoutez une variable pour la vitesse

    private AnimationController animationController;

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = speed;
        animationController = GetComponent<AnimationController>();

        enabled = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            // Effectue un raycast depuis la position de la souris vers le sol
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _ground))
            {
                // Définit la destination de l'agent NavMesh vers le point touché par le raycast
                myAgent.SetDestination(hit.point);

                animationController.isWalking = true;

            }
        }
    }
}