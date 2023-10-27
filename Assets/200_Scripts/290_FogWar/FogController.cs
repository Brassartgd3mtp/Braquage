using System.Collections;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [Header("Fog Settings")]
    public float targetFogDensity = 0.05f;  // Densité de fog cible lorsque le joueur est dans la zone.
    public float transitionSpeed = 1.0f;    // Vitesse de transition de la densité de fog.

    private float initialFogDensity;        // Densité de fog initiale.

    private void Start()
    {
        initialFogDensity = RenderSettings.fogDensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est entré dans la zone. Augmentez la densité de fog.
            StartCoroutine(ChangeFogDensity(targetFogDensity));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur a quitté la zone. Rétablissez la densité de fog initiale.
            StartCoroutine(ChangeFogDensity(initialFogDensity));
        }
    }

    private IEnumerator ChangeFogDensity(float targetDensity)
    {
        float currentDensity = RenderSettings.fogDensity;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionSpeed)
        {
            elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(currentDensity, targetDensity, elapsedTime / transitionSpeed);
            yield return null;
        }

        RenderSettings.fogDensity = targetDensity;
    }
}