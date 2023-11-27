// Ce script g�re le mouvement d'une unit� en utilisant NavMeshAgent dans un projet Unity.
// Il permet � l'unit� de se d�placer vers un point du sol lorsqu'un clic droit de la souris est effectu�.

using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    public LayerMask interactible;

    public float speed = 3.5f;

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                // D�finit la destination de l'agent NavMesh vers le point touch� par le raycast si le layer mask est ground
                myAgent.SetDestination(hit.point);

                animationController.isWalking = true;

            }
            //else if (Physics.Raycast(ray, out hit, interactible))
            //{
            //    Vector3 destination = hit.point;
            //    float stoppingDistance = 1.0f; // Adjust this value based on your preference
            //    destination = hit.point - ray.direction * stoppingDistance;
            //}
        }
    }
}