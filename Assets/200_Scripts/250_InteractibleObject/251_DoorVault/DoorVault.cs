using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVault : InteractibleObject
{
    //Savoir si la porte est crochet� ou non et si le crochetage est en cours
    private bool isLocked = true;
    private bool isBeingPicked = false;

    public float _pickingDuration = 5.0f; // Dur�e n�cessaire pour crocheter la porte, d�finit la difficult� du crochetage

    //R�cup�re la barre d'UI qui permet de voir o� en est le crochetage
    public BarProgressionDoorVault barProgressionUI;

    public GameObject _sparkParticle;


    private bool isOpening = false; // Bool�en pour d�terminer si la porte est en train de s'ouvrir
    public float _resolutionTime = 1.0f; // Dur�e de la transition en secondes

    private bool isTransitioning = false; // Bool�en pour v�rifier si la transition est en cours

    private Collider doorCollider; // R�f�rence au collider de la porte

    //Valeur de l'axe de rotation pour l'ouverture de la porte et la fermeture
    [SerializeField] private float axeYRotationToOpen = 0.0f;
    [SerializeField] private float axeYRotationToClose = 0.0f;


    void Start()
    {
        doorCollider = GetComponent<Collider>();
        LockDoor();
    }

    public override void OnInteraction()
    {
        if (!isBeingPicked && isLocked)
        {
            StartCoroutine(PickDoorVaultCoroutine());
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


    #region M�thode de crochetage avec condition
    void UnlockDoor()
    {
        isLocked = false;
    }

    void LockDoor()
    {
        isLocked = true;
    }

    System.Collections.IEnumerator PickDoorVaultCoroutine()
    {
        isBeingPicked = true;
        _sparkParticle.SetActive(true);
        yield return new WaitForSeconds(_pickingDuration);

        UnlockDoor();
        _sparkParticle.SetActive(false);
        isBeingPicked = false;
    }
    #endregion

    #region Methode d'ouverture et fermeture de la porte
    void OpenDoor()
    {
        //Permet d'ouvrir la porte sur un axe de rotation avec les Quaternion
        StartCoroutine(AnimateDoor(transform.rotation, Quaternion.Euler(0, axeYRotationToOpen, 0)));
    }

    void CloseDoor()
    {
        //Permet de fermer la porte sur un axe de rotation avec les Quaternion
        StartCoroutine(AnimateDoor(transform.rotation, Quaternion.Euler(0, axeYRotationToClose, 0)));
    }


    System.Collections.IEnumerator AnimateDoor(Quaternion startRotation, Quaternion endRotation)
    {
        isTransitioning = true; // Marque le d�but de la transition
        doorCollider.enabled = false; // D�sactive le collider au d�but de la transition pour �viter le spam de touche

        float elapsedTime = 0.0f;

        while (elapsedTime < _resolutionTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / _resolutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Assure que la rotation finale est correcte

        doorCollider.enabled = true; // R�active le collider � la fin de la transition

        isTransitioning = false; // Marque la fin de la transition
        isOpening = !isOpening; //Permet de savoir si la porte est en position ouverte ou ferm�e
    }
    #endregion
}
