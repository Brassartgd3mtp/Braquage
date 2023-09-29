using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent; // Composant NavMeshAgent pour g�rer le d�placement
    private Camera mainCamera; // Cam�ra principale pour d�tecter les clics

    [SerializeField] private float speed = 5f; // Vitesse de d�placement
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // D�finir la vitesse NavMeshAgent
        agent.speed = speed;

        // Si le bouton gauche de la souris est enfonc�
        if (Input.GetMouseButtonDown(0))
        {
            // Lancer un rayon depuis la position de la souris sur l'�cran
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // V�rifier s'il y a une collision avec le terrain
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // D�finir la destination du NavMeshAgent sur le point d'impact
                agent.SetDestination(hit.point);
            }
        }
    }
}

