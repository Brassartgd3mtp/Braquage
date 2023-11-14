using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static List<string> objectTag = new List<string>();
    [SerializeField] private InteractibleObjectV2 interactibleObject; //Script de l'objet interactif
    [SerializeField] private bool isKeyPressed = false;



    private void Start()
    {
        Invoke("SetupTags", 1f);
    }

    private void SetupTags()
    {
        objectTag.Add("Interactible");
        objectTag.Add("Goal");
    }

    void Update()
    {
        // V�rifie si une touche sp�cifique est enfonc�e
        if (isKeyPressed && Input.GetKeyUp(KeyCode.Space))
        {
            if (interactibleObject != null)
            {
                // Appelle la m�thode lorsque la touche est enfonc�e
                interactibleObject.OnInteraction(gameObject);

            }
            else
            {
                Debug.LogError("InteractibleObject non trouv� sur l'objet.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (objectTag.Contains(other.tag))
        {
            // D�finis la variable pour permettre l'activation de la m�thode
            isKeyPressed = true;

            interactibleObject = other.GetComponent<InteractibleObjectV2>();
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