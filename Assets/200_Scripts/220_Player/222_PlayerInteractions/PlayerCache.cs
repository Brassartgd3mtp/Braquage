using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCache : MonoBehaviour
{
    public KeyCode hideKey = KeyCode.H;
    public float hideDistance = 5f;
    public LayerMask hideLayer;
    public bool isHidden = false;

    // Ajoutez une variable pour stocker le matériau d'origine
    public Material transparencyMaterial;

    public float alphaColor;

    void Start()
    {
        // Assurez-vous d'avoir un Renderer attaché au GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Assurez-vous que le Renderer a un matériau
        if (renderer != null && renderer.material != null)
        {
            // Stockez le matériau d'origine
            transparencyMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un matériau.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            // Vérifiez si une cachette est à proximité
            Collider[] nearbyHidingSpots = Physics.OverlapSphere(transform.position, hideDistance, hideLayer);

            foreach (Collider hidingSpot in nearbyHidingSpots)
            {
                isHidden = !isHidden;

                // Appel de la fonction SetHiddenState pour gérer la transparence
                SetHiddenState(isHidden);

                // Sortez de la boucle dès que vous trouvez une cachette
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

                // Créez une copie du matériau d'origine pour pouvoir le modifier
                Material transparentMaterial = new Material(transparencyMaterial);
                Color baseColor = transparentMaterial.color;

                // Modifiez l'alpha de la couleur pour rendre le joueur transparent
                baseColor.a = alphaColor;

                transparentMaterial.color = baseColor;

                // Appliquez le matériau transparent au Renderer
                renderer.material = transparentMaterial;
            }
            else
            {
                Debug.Log("Revealed to guard!");

                // Si le joueur n'est plus caché, rétablissez le matériau d'origine
                renderer.material = transparencyMaterial;
            }
        }
        else
        {
            Debug.LogError("Le GameObject doit avoir un Renderer avec un matériau.");
        }
    }

    // Dessine une sphère de gizmo pour visualiser la zone de cachement dans l'éditeur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hideDistance);
    }
}
