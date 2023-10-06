using UnityEngine;

public class CrochetDoor : InteractibleObject
{
    private bool isLocked = true;
    private bool isBeingPicked = false;

    public float _pickingDuration = 5.0f; // Dur�e n�cessaire pour crocheter la porte

    public GameObject barProgressionUI;

    private Collider doorCollider; // R�f�rence au collider de la porte


    private bool isOpening = false; // Bool�en pour d�terminer si la porte est en train de s'ouvrir
    private bool isTransitioning = false; // Bool�en pour v�rifier si la transition est en cours


    public float _resolutionTime = 1.0f; // Dur�e de la transition en secondes

    //Axe d'ouverture de la porte
    [SerializeField] private float axeXOpening = 4.0f;
    [SerializeField] private float axeYOpening = 0.0f;
    [SerializeField] private float axeZOpening = 0.0f;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
        LockDoor();
    }


    public override void OnInteraction()
    {
        if (!isBeingPicked && isLocked)
        {
            StartCoroutine(PickDoorCoroutine());
            BarProgression barProgression = barProgressionUI.GetComponent<BarProgression>();
            barProgression.AugmenterFillAmount();
        }


        else if (!isTransitioning)
        {
            if (!isLocked)
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
    }


    #region Lock System
    void UnlockDoor()
    {
        isLocked = false;

        Debug.Log("La porte est d�verrouill�.");
    }

    void LockDoor()
    {
        isLocked = true;

        Debug.Log("La porte est verrouill�.");
    }

    System.Collections.IEnumerator PickDoorCoroutine()
    {
        isBeingPicked = true;

        // Attends la dur�e n�cessaire pour crocheter la porte
        yield return new WaitForSeconds(_pickingDuration);

        Debug.Log("La porte a �t� crochet� !");

        UnlockDoor();
        isBeingPicked = false;

    }
    #endregion


    #region OpendDoor System
    void OpenDoor()
    {
        // Code pour ouvrir la porte de droite � gauche
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(-axeXOpening, -axeYOpening, -axeZOpening)));

        Debug.Log("La porte s'ouvre de droite � gauche");
    }

    void CloseDoor()
    {
        // Code pour fermer la porte de gauche � droite
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(axeXOpening, axeYOpening, axeZOpening)));

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
    #endregion
}
