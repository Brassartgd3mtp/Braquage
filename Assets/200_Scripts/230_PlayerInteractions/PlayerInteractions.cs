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
        // V�rifie si une touche sp�cifique est enfonc�e
        if (isKeyPressed && Input.GetKeyUp(KeyCode.T))
        {
            if (interactibleObject != null)
            {
                interactibleObject.OnInteraction();
                // Appelle la m�thode lorsque la touche est enfonc�e
            }
            else
            {
                Debug.LogError("InteractibleObject non trouv� sur l'objet.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            // D�finis la variable pour permettre l'activation de la m�thode
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
        // Remets la variable � false pour ne pas r�p�ter l'appel de la m�thode
        isKeyPressed = false;

        //Vide la variable interactibleObject
        interactibleObject = null;
    }
}

