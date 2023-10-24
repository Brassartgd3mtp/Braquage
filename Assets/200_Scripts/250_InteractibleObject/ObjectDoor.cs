using UnityEngine;

public class ObjectDoor : InteractibleObject
{
    private bool isOpening = false; // Booléen pour déterminer si la porte est en train de s'ouvrir
    public float _resolutionTime = 1.0f; // Durée de la transition en secondes

    private bool isTransitioning = false; // Booléen pour vérifier si la transition est en cours

    private Collider doorCollider; // Référence au collider de la porte

    [SerializeField] private float axeXOpening = 4.0f;
    [SerializeField] private float axeYOpening = 0.0f;
    [SerializeField] private float axeZOpening = 0.0f;
    [SerializeField] AudioDatabase audioDatabase;


    void Start()
    {
        doorCollider = GetComponent<Collider>();
    }

    public override void OnInteraction()
    {
        base.OnInteraction(); // Appelle d'abord la méthode du script parent si nécessaire
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
    }

    void OpenDoor()
    {
        // Code pour ouvrir la porte de droite à gauche
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(-axeXOpening, -axeYOpening, -axeZOpening)));
        audioDatabase.PlayDoorOpen(transform.position, 1.0f); // Appelle de le son d'ouverture de la porte
    }

    void CloseDoor()
    {
        // Code pour fermer la porte de gauche à droite
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(axeXOpening, axeYOpening, axeZOpening)));
        audioDatabase.PlayDoorClose(transform.position, 1.0f); // Appelle de le son de fermeture de la porte
    }


    System.Collections.IEnumerator AnimateDoor(Vector3 start, Vector3 end)
    {
        isTransitioning = true; // Marque le début de la transition
        //doorCollider.enabled = false; // Désactive le collider au début de la transition

        float elapsedTime = 0.0f;

        while (elapsedTime < _resolutionTime)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / _resolutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Assure que la position finale est correcte

        //doorCollider.enabled = true; // Réactive le collider à la fin de la transition

        isTransitioning = false; // Marque la fin de la transition
        isOpening = !isOpening;
    }

}
