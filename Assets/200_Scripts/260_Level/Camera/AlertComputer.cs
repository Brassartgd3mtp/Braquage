using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertComputer : InteractibleObjectV2
{
    public GameObject targetObject;
    public GameObject cameraAnimation;
    public bool activate = true;
    private float currentAnimationTime = 0f; // Variable pour sauvegarder la position de lecture de l'animation

    public KeyCode disableKey = KeyCode.Space; // Touche pour désactiver le script

    //public override void OnInteraction(GameObject interactingPlayer)
    void Update()
    {
        if (Input.GetKeyDown(disableKey))
        {
            // Assurez-vous que le GameObject cible est défini
            if (targetObject != null)
            {
                // Essayez de récupérer le script et l'animation sur le GameObject cible
                MonoBehaviour scriptToDisable = targetObject.GetComponent<MonoBehaviour>();
                Animation animation = cameraAnimation.GetComponent<Animation>();

                // Si le script est trouvé, désactivez-le
                if (scriptToDisable != null && activate == true)
                {
                    scriptToDisable.enabled = !scriptToDisable.enabled;
                    Debug.Log("Script disabled on targetObject.");

                    // Sauvegardez la position actuelle de l'animation avant de l'arrêter
                    if (!scriptToDisable.enabled)
                    {
                        currentAnimationTime = animation[animation.clip.name].time;
                        animation.Stop();
                    }
                    else
                    {
                        // Relancez l'animation et restaurez la position de lecture
                        animation.Play();
                        animation[animation.clip.name].time = currentAnimationTime;
                    }
                }
                else
                {
                    Debug.LogWarning("Script not found on targetObject.");
                }
            }
            else
            {
                Debug.LogWarning("Target object not assigned.");
            }
        }
    }
}
