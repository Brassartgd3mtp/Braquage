using System.Collections.Generic;
using UnityEngine;

public class CrochetDoor : InteractibleObject
{
    //Savoir si la porte est crocheté ou non et si le crochetage est en cours
    private bool isLocked = true;
    private bool isBeingPicked = false;

    public float _pickingDuration = 5.0f; // Durée nécessaire pour crocheter la porte
    
    //Récupère le script de rôle des personnage pour en récupérer le _pickingMultiplier
    public List<PlayerRole> _playerRole = new List<PlayerRole>();
    public float _totalPickingTime;

    //Récupère la barre d'UI qui permet de voir où en est le crochetage
    public GameObject barProgressionUI;

    private Collider doorCollider; // Référence au collider de la porte


    private bool isOpening = false; // Booléen pour déterminer si la porte ouverte ou fermée
    private bool isTransitioning = false; // Booléen pour vérifier si la transition est en cours


    public float _resolutionTime = 1.0f; // Durée de la transition en secondes

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

        Debug.Log("La porte est déverrouillé.");
    }

    void LockDoor()
    {
        isLocked = true;

        Debug.Log("La porte est verrouillé.");
    }

    #region Partie de script qui récupère le rôle et le multiplicateur de crochetage _pickingMultiplier
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider est celui du joueur (ajustez selon vos besoins)
        if (other.CompareTag("Player"))
        {
            // Ajoute le script du joueur à la liste s'il n'est pas déjà présent
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && !_playerRole.Contains(_role))
            {
                _playerRole.Add(_role);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le collider est celui du joueur et le retire de la liste s'il est présent
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

        // Attends la durée nécessaire pour crocheter la porte
        foreach (PlayerRole playerScript in _playerRole)
        {
            Debug.Log("_pickingMultiplier: " + playerScript._pickingMultiplier);
            _totalPickingTime += playerScript._pickingMultiplier * _pickingDuration;

        }
        Debug.Log("La porte a été crocheté !");

            yield return new WaitForSeconds(_totalPickingTime);
        UnlockDoor();
        isBeingPicked = false;

    }
    #endregion


    #region OpendDoor System
    void OpenDoor()
    {
        // Code pour ouvrir la porte de droite à gauche
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(-axeXOpening, -axeYOpening, -axeZOpening)));

        Debug.Log("La porte s'ouvre de droite à gauche");
    }

    void CloseDoor()
    {
        // Code pour fermer la porte de gauche à droite
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(axeXOpening, axeYOpening, axeZOpening)));

        Debug.Log("La porte se ferme de gauche à droite");
    }

    System.Collections.IEnumerator AnimateDoor(Vector3 start, Vector3 end)
    {
        isTransitioning = true; // Marque le début de la transition
        doorCollider.enabled = false; // Désactive le collider au début de la transition

        float elapsedTime = 0.0f;

        while (elapsedTime < _resolutionTime)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / _resolutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Assure que la position finale est correcte

        doorCollider.enabled = true; // Réactive le collider à la fin de la transition

        isTransitioning = false; // Marque la fin de la transition
        isOpening = !isOpening;
    }
    #endregion
}
