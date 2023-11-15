using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertComputer : InteractibleObjectV2
{
    [HideInInspector] public List<GameObject> securityCameras;
    [HideInInspector] public List<GameObject> sphereCameras;
    public bool activate = true;
    private float currentAnimationTime = 0f; // Variable pour sauvegarder la position de lecture de l'animation

    public KeyCode disableKey = KeyCode.Space; // Touche pour désactiver le script

    void Start()
    {
        // Ajoute les objets avec le layer "Camera" à la liste securityCameras
        AddCamerasByLayer("Camera");
        AddSphereCamerasByLayer("Sphere");
    }


    //public override void OnInteraction(GameObject interactingPlayer)
    void Update()
    {
        if (Input.GetKeyDown(disableKey))
        {
            if (securityCameras != null && securityCameras.Count > 0)
            {
                // Pour chaque objet dans la liste
                foreach (GameObject cameraObject in securityCameras)
                {
                    // Essaie de récupérer le script et l'animation sur le GameObject cible
                    MonoBehaviour scriptToDisable = cameraObject.GetComponentInChildren<MonoBehaviour>();
                    Animation animation = cameraObject.GetComponentInChildren<Animation>();

                    // Si le script est trouvé, désactivez-le
                    if (scriptToDisable != null && activate == true)
                    {
                        scriptToDisable.enabled = !scriptToDisable.enabled;

                        if (!scriptToDisable.enabled)
                        {
                            // Sauvegardez la position actuelle de l'animation avant de l'arrêter
                            currentAnimationTime = animation[animation.clip.name].time;
                            animation.Stop();
                            ChangeColorOfObjects(sphereCameras, Color.black);
                            //SetListActive(sphereCameras, false);
                        }
                        else
                        {
                            // Relancez l'animation et restaurez la position de lecture
                            animation.Play();
                            animation[animation.clip.name].time = currentAnimationTime;
                            ChangeColorOfObjects(sphereCameras, Color.red);
                            //SetListActive(sphereCameras, true);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Script not found on targetObject.");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Target object not assigned.");
            }
        }
    }


    void AddCamerasByLayer(string layerName)
    {
        // Trouve tous les GameObjects dans la scène
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Ajoute les objets avec le layer spécifié à la liste securityCameras
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(layerName))
            {
                securityCameras.Add(obj);
            }
        }
    }
    void AddSphereCamerasByLayer(string layerName)
    {
        // Trouve tous les GameObjects dans la scène
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Ajoute les objets avec le layer spécifié à la liste sphereCameras
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(layerName))
            {
                sphereCameras.Add(obj);
            }
        }
    }
    void SetListActive(List<GameObject> gameObjects, bool isActive)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
            else
            {
                Debug.LogWarning("One of the GameObjects in the list is null.");
            }
        }
    }

    void ChangeColorOfObjects(List<GameObject> objects, Color newColor)
    {
        foreach (GameObject obj in objects)
        {
            // Assure-toi que l'objet n'est pas null
            if (obj != null)
            {
                // Récupère le composant Renderer
                Renderer renderer = obj.GetComponent<Renderer>();

                // Vérifie si le composant Renderer existe
                if (renderer != null)
                {
                    // Modifie la couleur du matériau
                    renderer.material.color = newColor;
                }
                else
                {
                    Debug.LogWarning("Renderer component not found on GameObject: " + obj.name);
                }
            }
            else
            {
                Debug.LogWarning("One of the GameObjects in the list is null.");
            }
        }
    }
}
