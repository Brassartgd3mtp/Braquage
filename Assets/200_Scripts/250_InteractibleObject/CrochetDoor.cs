using System.Collections.Generic;
using UnityEngine;

public class CrochetDoor : InteractibleObject
{
    //Savoir si la porte est crochet� ou non et si le crochetage est en cours
    private bool isLocked = true;
    private bool isBeingPicked = false;

    public float _pickingDuration = 5.0f; // Dur�e n�cessaire pour crocheter la porte
    
    //R�cup�re le script de r�le des personnage pour en r�cup�rer le _pickingMultiplier
    public List<PlayerRole> _playerRole = new List<PlayerRole>();
    public float _totalPickingTime;

    //R�cup�re la barre d'UI qui permet de voir o� en est le crochetage
    public GameObject barProgressionUI;

    private Collider doorCollider; // R�f�rence au collider de la porte


    private bool isOpening = false; // Bool�en pour d�terminer si la porte ouverte ou ferm�e
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

    #region Partie de script qui r�cup�re le r�le et le multiplicateur de crochetage _pickingMultiplier
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le collider est celui du joueur (ajustez selon vos besoins)
        if (other.CompareTag("Player"))
        {
            // Ajoute le script du joueur � la liste s'il n'est pas d�j� pr�sent
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && !_playerRole.Contains(_role))
            {
                _playerRole.Add(_role);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le collider est celui du joueur et le retire de la liste s'il est pr�sent
        if (other.CompareTag("Player"))
        {
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && _playerRole.Contains(_role))
            {
                _playerRole.Remove(_role);
            }
        }
    }
    #endregion
    System.Collections.IEnumerator PickDoorCoroutine()
    {
        isBeingPicked = true;

        // Attends la dur�e n�cessaire pour crocheter la porte
        foreach (PlayerRole playerScript in _playerRole)
        {
            Debug.Log("_pickingMultiplier: " + playerScript._pickingMultiplier);
            _totalPickingTime += playerScript._pickingMultiplier * _pickingDuration;

        }
        Debug.Log("La porte a �t� crochet� !");

            yield return new WaitForSeconds(_totalPickingTime);
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
