//using UnityEngine;
//
//public class DisappearOnContact : MonoBehaviour
//{
//    public float disappearanceDuration = 2.0f; // Durée en secondes pour la disparition
//
//    private void OnTriggerEnter(Collider other)
//    {
//        // Vérifie si le collider est celui du joueur (ajustez selon vos besoins)
//        if (other.CompareTag("Player"))
//        {
//            // Démarre la coroutine pour faire disparaître l'objet
//            StartCoroutine(DisappearObject());
//        }
//    }
//
//    System.Collections.IEnumerator DisappearObject()
//    {
//        float elapsedTime = 0.0f;
//        Color initialColor = GetComponent<Renderer>().material.color;
//
//        while (elapsedTime < disappearanceDuration)
//        {
//            // Interpole la transparence de l'objet de sa valeur actuelle vers 0 (complètement transparent)
//            float alpha = Mathf.Lerp(initialColor.a, 0.0f, elapsedTime / disappearanceDuration);
//
//            // Applique la nouvelle couleur à l'objet
//            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
//
//            // Attends un petit moment avant la prochaine itération
//            yield return null;
//
//            // Met à jour le temps écoulé
//            elapsedTime += Time.deltaTime;
//        }
//
//        // Détruit l'objet une fois qu'il a disparu
//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class DisappearReappearOnContact : MonoBehaviour
{
    public float disappearanceDuration = 2.0f; // Durée en secondes pour la disparition
    public float reappearDuration = 2.0f; // Durée en secondes pour la réapparition

    private Coroutine disappearCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider est celui du joueur (ajustez selon vos besoins)
        if (other.CompareTag("Player"))
        {
            // Démarre la coroutine pour faire disparaître l'objet
            disappearCoroutine = StartCoroutine(DisappearObject());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le collider n'est plus celui du joueur
        if (other.CompareTag("Player"))
        {
            // Arrête la coroutine pour faire disparaître l'objet
            if (disappearCoroutine != null)
            {
                StopCoroutine(disappearCoroutine);
            }

            // Démarre la coroutine pour faire réapparaître l'objet
            StartCoroutine(ReappearObject());
        }
    }

    System.Collections.IEnumerator DisappearObject()
    {
        float elapsedTime = 0.0f;
        Color initialColor = GetComponent<Renderer>().material.color;

        while (elapsedTime < disappearanceDuration)
        {
            // Interpole la transparence de l'objet de sa valeur actuelle vers 0 (complètement transparent)
            float alpha = Mathf.Lerp(initialColor.a, 0.0f, elapsedTime / disappearanceDuration);

            // Applique la nouvelle couleur à l'objet
            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // Attends un petit moment avant la prochaine itération
            yield return null;

            // Met à jour le temps écoulé
            elapsedTime += Time.deltaTime;
        }

        // L'objet est maintenant complètement transparent
    }

    System.Collections.IEnumerator ReappearObject()
    {
        float elapsedTime = 0.0f;
        Color initialColor = GetComponent<Renderer>().material.color;

        while (elapsedTime < reappearDuration)
        {
            // Interpole la transparence de l'objet de sa valeur actuelle vers 1 (complètement opaque)
            float alpha = Mathf.Lerp(initialColor.a, 1.0f, elapsedTime / reappearDuration);

            // Applique la nouvelle couleur à l'objet
            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // Attends un petit moment avant la prochaine itération
            yield return null;

            // Met à jour le temps écoulé
            elapsedTime += Time.deltaTime;
        }
    }
}
