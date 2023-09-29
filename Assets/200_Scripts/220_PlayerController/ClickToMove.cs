using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent; // Composant NavMeshAgent pour gérer le déplacement
    private Camera mainCamera; // Caméra principale pour détecter les clics

    [SerializeField] private float speed = 5f; // Vitesse de déplacement
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Définir la vitesse NavMeshAgent
        agent.speed = speed;

        // Si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            // Lancer un rayon depuis la position de la souris sur l'écran
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vérifier s'il y a une collision avec le terrain
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // Définir la destination du NavMeshAgent sur le point d'impact
                agent.SetDestination(hit.point);
            }
        }
    }
}

