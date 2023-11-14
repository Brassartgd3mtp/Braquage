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
            // V�rifiez si une cachette est � proximit�
            Collider[] nearbyHidingSpots = Physics.OverlapSphere(transform.position, hideDistance, hideLayer);

            foreach (Collider hidingSpot in nearbyHidingSpots)
            {
                isHidden = !isHidden;

                if (isHidden)
                {
                    Debug.Log("Hiding from guard!");
                    // Ajoutez ici le code pour d�sactiver le rendu du joueur ou effectuer d'autres actions de cachement
                }
                else
                {
                    Debug.Log("Revealed to guard!");
                    // Ajoutez ici le code pour r�activer le rendu du joueur ou effectuer d'autres actions apr�s avoir �t� r�v�l�
                }
                return; // Sortez de la boucle d�s que vous trouvez une cachette, le joueur ne peut se cacher que dans une cachette � la fois
            }
        }
    }

    // Dessine une sph�re de gizmo pour visualiser la zone de cachement dans l'�diteur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hideDistance);
    }
}
