using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public string objectTag = "NomDuTag"; // Remplace "NomDuTag" par le tag de ton objet
    [SerializeField] private InteractibleObject interactibleObject; //Script de l'objet interactif
    [SerializeField] private bool isKeyPressed = false;


    void Update()
    {
        // Vérifie si une touche spécifique est enfoncée
        if (isKeyPressed && Input.GetKeyUp(KeyCode.T))
        {
            if (interactibleObject != null)
            {
                interactibleObject.OnInteraction();
                // Appelle la méthode lorsque la touche est enfoncée
            }
            else
            {
                Debug.LogError("InteractibleObject non trouvé sur l'objet.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            // Définis la variable pour permettre l'activation de la méthode
            isKeyPressed = true;

            interactibleObject = other.GetComponent<InteractibleObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchKeyFalse();
    }

    void SwitchKeyFalse()
    {
        // Remets la variable à false pour ne pas répéter l'appel de la méthode
        isKeyPressed = false;

        //Vide la variable interactibleObject
        interactibleObject = null;
    }
}

