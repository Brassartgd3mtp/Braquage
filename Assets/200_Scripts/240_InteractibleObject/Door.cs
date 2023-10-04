using UnityEngine;

public class Door : InteractibleObject
{
    private bool isOpening = false; // Bool�en pour d�terminer si la porte est en train de s'ouvrir
    public float _resolutionTime = 1.0f; // Dur�e de la transition en secondes

    private bool isTransitioning = false; // Bool�en pour v�rifier si la transition est en cours

    private Collider doorCollider; // R�f�rence au collider de la porte

    void Start()
    {
        doorCollider = GetComponent<Collider>();
    }


    void OpenDoor()
    {
        // Code pour ouvrir la porte de droite � gauche
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(-4.0f, 0.0f, 0.0f)));

        Debug.Log("La porte s'ouvre de droite � gauche");
    }

    void CloseDoor()
    {
        // Code pour fermer la porte de gauche � droite
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(4.0f, 0.0f, 0.0f)));

        Debug.Log("La porte se ferme de gauche � droite");
    }


    System.Collections.IEnumerator AnimateDoor(Vector3 start, Vector3 end)
    {
        isTransitioning = true; // Marque le d�but de la transition
        doorCollider.enabled = false; // D�sactive le collider au d�but de la transition

        float elapsedTime = 0.0f;

        while (elapsedTime < _resolutionTime)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / _resolutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Assure que la position finale est correcte

        doorCollider.enabled = true; // R�active le collider � la fin de la transition

        isTransitioning = false; // Marque la fin de la transition
        isOpening = !isOpening;
    }

    public override void OnInteraction()
    {
        base.OnInteraction(); // Appelle d'abord la m�thode du script parent si n�cessaire
        if (!isTransitioning)
        {
            if (isOpening)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        // Inverse la valeur du bool�en pour la prochaine fois
        Debug.Log("Contact Enfant");
        // Ajoute le code sp�cifique � cet objet interactif
    }
}
