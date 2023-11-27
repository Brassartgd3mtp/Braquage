using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCache : MonoBehaviour
{
    public KeyCode hideKey = KeyCode.H;
    public float hideDistance = 5f;
    public LayerMask hideLayer;
    public bool isHidden = false;

    // Ajoutez une variable pour stocker le mat�riau d'origine
    public Material transparencyMaterial;

    public float alphaColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && renderer.material != null)
        {
            transparencyMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un mat�riau.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            // V�rifie si une cachette est � proximit�
            Collider[] nearbyHidingSpots = Physics.OverlapSphere(transform.position, hideDistance, hideLayer);

            foreach (Collider hidingSpot in nearbyHidingSpots)
            {
                isHidden = !isHidden;

                // Appel de la fonction SetHiddenState pour g�rer la transparence
                SetHiddenState(isHidden);

                // Sort de la boucle d�s qu'une cachette est � proximit�
                return;
            }
        }
    }

    public void SetHiddenState(bool hidden)
    {
        isHidden = hidden;

        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && transparencyMaterial != null)
        {
            if (isHidden)
            {
                Debug.Log("Hiding from guard!");

                // Cr�ez une copie du mat�riau d'origine pour pouvoir le modifier
                Material transparentMaterial = new Material(transparencyMaterial);
                Color baseColor = transparentMaterial.color;

                // Modifiez l'alpha de la couleur pour rendre le joueur transparent
                baseColor.a = alphaColor;

                transparentMaterial.color = baseColor;

                // Appliquez le mat�riau transparent au Renderer
                renderer.material = transparentMaterial;
            }
            else
            {
                Debug.Log("Revealed to guard!");

                // Si le joueur n'est plus cach�, r�tablissez le mat�riau d'origine
                renderer.material = transparencyMaterial;
            }
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un mat�riau.");
        }
    }

    // Dessine une sph�re de gizmo pour visualiser la zone dans l'�diteur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hideDistance);
    }
}
