using System.Collections.Generic;
using UnityEngine;

public class CrochetDoor : InteractibleObject
{
    private Collider doorCollider; // Référence au collider de la porte
    private bool isLocked = true; //Bool si la porte est verrouillée
    private bool isBeingPicked = false; //Bool si le crochetage est effectué
    private bool isOpening = false; //Bool si la porte est ouverte ou fermée
    private bool isTransitioning = false; //Bool si la porte est en train de s'ouvrir ou se fermer

    //Récupère le script de rôle des personnage pour en récupérer le _pickingMultiplier
    public List<PlayerRole> _playerRole = new List<PlayerRole>();
    //Récupère la barre d'UI qui permet de voir où en est le crochetage
    public BarProgression barProgressionUI;

    [Header("Variable")]
    public float _pickingDuration = 5.0f; // Durée nécessaire pour crocheter la porte, ça définit la difficulté du crochetage
    public float _resolutionTime = 1.0f; // Durée de la transition en secondes

    [HideInInspector]
    public float _totalPickingTime; //Durée total du crochetage en fonction du role du player

    [Header("Axe d'ouverture")]
    //Axe d'ouverture de la porte
    public float axeXOpening = 4.0f;
    public float axeYOpening = 0.0f;
    public float axeZOpening = 0.0f;


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
            barProgressionUI.AugmenterFillAmount();

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
    }

    void LockDoor()
    {
        isLocked = true;
    }

    #region Partie de script qui récupère le rôle et le multiplicateur de crochetage _pickingMultiplier

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider est celui du joueur
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

        // Attends la durée nécessaire pour crocheter la porte en fonction de la dureté du crochetage et du rôle du joueur qui effectue l'action
        foreach (PlayerRole roleScript in _playerRole)
        {
            _totalPickingTime += roleScript._pickingMultiplier * _pickingDuration;
        }

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
    }

    void CloseDoor()
    {
        // Code pour fermer la porte de gauche à droite
        StartCoroutine(AnimateDoor(transform.position, transform.position + new Vector3(axeXOpening, axeYOpening, axeZOpening)));
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

    #endregion
}