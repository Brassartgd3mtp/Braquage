using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCache : MonoBehaviour
{
    public KeyCode hideKey = KeyCode.H;
    public float hideDistance = 5f;
    public LayerMask hideLayer;
    public bool isHidden = false;

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            // Vérifiez si une cachette est à proximité
            Collider[] nearbyHidingSpots = Physics.OverlapSphere(transform.position, hideDistance, hideLayer);

            foreach (Collider hidingSpot in nearbyHidingSpots)
            {
                isHidden = !isHidden;

                if (isHidden)
                {
                    Debug.Log("Hiding from guard!");
                    // Ajoutez ici le code pour désactiver le rendu du joueur ou effectuer d'autres actions de cachement
                }
                else
                {
                    Debug.Log("Revealed to guard!");
                    // Ajoutez ici le code pour réactiver le rendu du joueur ou effectuer d'autres actions après avoir été révélé
                }
                return; // Sortez de la boucle dès que vous trouvez une cachette, le joueur ne peut se cacher que dans une cachette à la fois
            }
        }
    }

    // Dessine une sphère de gizmo pour visualiser la zone de cachement dans l'éditeur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hideDistance);
    }
}
