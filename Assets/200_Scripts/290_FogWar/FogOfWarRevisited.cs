//using UnityEngine;
//
//public class DisappearOnContact : MonoBehaviour
//{
//    public float disappearanceDuration = 2.0f; // Dur�e en secondes pour la disparition
//
//    private void OnTriggerEnter(Collider other)
//    {
//        // V�rifie si le collider est celui du joueur (ajustez selon vos besoins)
//        if (other.CompareTag("Player"))
//        {
//            // D�marre la coroutine pour faire dispara�tre l'objet
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
//            // Interpole la transparence de l'objet de sa valeur actuelle vers 0 (compl�tement transparent)
//            float alpha = Mathf.Lerp(initialColor.a, 0.0f, elapsedTime / disappearanceDuration);
//
//            // Applique la nouvelle couleur � l'objet
//            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
//
//            // Attends un petit moment avant la prochaine it�ration
//            yield return null;
//
//            // Met � jour le temps �coul�
//            elapsedTime += Time.deltaTime;
//        }
//
//        // D�truit l'objet une fois qu'il a disparu
//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class DisappearReappearOnContact : MonoBehaviour
{
    public float disappearanceDuration = 2.0f; // Dur�e en secondes pour la disparition
    public float reappearDuration = 2.0f; // Dur�e en secondes pour la r�apparition

    private Coroutine disappearCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le collider est celui du joueur (ajustez selon vos besoins)
        if (other.CompareTag("Player"))
        {
            // D�marre la coroutine pour faire dispara�tre l'objet
            disappearCoroutine = StartCoroutine(DisappearObject());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le collider n'est plus celui du joueur
        if (other.CompareTag("Player"))
        {
            // Arr�te la coroutine pour faire dispara�tre l'objet
            if (disappearCoroutine != null)
            {
                StopCoroutine(disappearCoroutine);
            }

            // D�marre la coroutine pour faire r�appara�tre l'objet
            StartCoroutine(ReappearObject());
        }
    }

    System.Collections.IEnumerator DisappearObject()
    {
        float elapsedTime = 0.0f;
        Color initialColor = GetComponent<Renderer>().material.color;

        while (elapsedTime < disappearanceDuration)
        {
            // Interpole la transparence de l'objet de sa valeur actuelle vers 0 (compl�tement transparent)
            float alpha = Mathf.Lerp(initialColor.a, 0.0f, elapsedTime / disappearanceDuration);

            // Applique la nouvelle couleur � l'objet
            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // Attends un petit moment avant la prochaine it�ration
            yield return null;

            // Met � jour le temps �coul�
            elapsedTime += Time.deltaTime;
        }

        // L'objet est maintenant compl�tement transparent
    }

    System.Collections.IEnumerator ReappearObject()
    {
        float elapsedTime = 0.0f;
        Color initialColor = GetComponent<Renderer>().material.color;

        while (elapsedTime < reappearDuration)
        {
            // Interpole la transparence de l'objet de sa valeur actuelle vers 1 (compl�tement opaque)
            float alpha = Mathf.Lerp(initialColor.a, 1.0f, elapsedTime / reappearDuration);

            // Applique la nouvelle couleur � l'objet
            GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // Attends un petit moment avant la prochaine it�ration
            yield return null;

            // Met � jour le temps �coul�
            elapsedTime += Time.deltaTime;
        }
    }
}
