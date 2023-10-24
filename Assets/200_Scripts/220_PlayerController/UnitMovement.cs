// Ce script g�re le mouvement d'une unit� en utilisant NavMeshAgent dans un projet Unity.
// Il permet � l'unit� de se d�placer vers un point du sol lorsqu'un clic droit de la souris est effectu�.

using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask _ground;
    public float _speed = 3.5f; // Ajoutez une variable pour la vitesse


    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = _speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            // Effectue un raycast depuis la position de la souris vers le sol
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,_ground))
            {
                // D�finit la destination de l'agent NavMesh vers le point touch� par le raycast
                myAgent.SetDestination(hit.point);
            }
        }
    }
}