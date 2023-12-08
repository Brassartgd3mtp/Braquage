using System.Collections.Generic;
using UnityEngine;

public class ObjectDoorV2 : InteractibleObjectV2
{
    public bool isOpening = false;
    private bool isTransitioning = false;
    private PlayerRole interactingPlayer;

    [Header("Sp�cifique au Door simple")]
    // Couleurs de carte d'acc�s n�cessaires
    public bool requiresAccessCard = false;
    public bool requiresRedCard = false;
    public bool requiresBlueCard = false;
    public bool requiresGreenCard = false;

    [Header("Nom animation")]
    public string nameOpenAnimation;
    public string nameCloseAnimation;
    public string nameCardAnimation;


    [HideInInspector] public bool isLocked = true; //Bool si la porte est verrouill�e
    [HideInInspector] public bool isBeingPicked = false; //Bool si le crochetage est effectu�
    [HideInInspector] public List<PlayerRole> _playerRole = new List<PlayerRole>(); //R�cup�re le script de r�le des personnage pour en r�cup�rer le _pickingMultiplier

    [Header("Difficult� en seconde")]
    public float pickingDuration = 5.0f; // Dur�e n�cessaire pour crocheter la porte, �a d�finit la difficult� du crochetage
    [HideInInspector] public float totalPickingTime; //Dur�e total du crochetage en fonction du role du player

    [Header("GameObject")]
    public BarProgressionV2 barProgressionUI; //R�cup�re la barre d'UI qui permet de voir o� en est le crochetage
    public GameObject sparkParticle;

    [HideInInspector] public float raycastDistance = 10f; // Distance du raycast pour d�tecter les joueurs

    public Animation doorAnimation;
    public GameObject AccessCardText;
    public GameObject SecurityPass;



    void Start()
    {
        doorAnimation = GetComponent<Animation>();

        if (doorAnimation == null)
        {
            Debug.LogError("Animation component is not assigned to ObjectDoorV2.");
            return;
        }

        if (AccessCardText != null)
        {
            AccessCardText.SetActive(false);
        }

        if (SecurityPass != null && !requiresAccessCard)
        {
            SecurityPass.SetActive(false);
        }
    }

    public override void OnInteraction(GameObject interactablePlayer)
    {
        if (!requiresAccessCard)
        {
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
        else if (requiresAccessCard && CheckAccessCard())
        {
            OpenDoor();
        }
    }

    bool CheckAccessCard()
    {
        // V�rifie si le joueur qui interagit avec la porte d�tient la carte d'acc�s appropri�e
        if (interactingPlayer != null)
        {
            // V�rifie si le joueur a la carte
            if ((!requiresRedCard || (interactingPlayer.RedCard))
                && (!requiresBlueCard || (interactingPlayer.blueCard))
                && (!requiresGreenCard || (interactingPlayer.greenCard)))
            {
                // Marque la carte comme utilis�e une fois qu'elle a �t� utilis�e
                doorAnimation.Play(nameCardAnimation);
                requiresAccessCard = false;
                requiresBlueCard = false;
                requiresGreenCard = false;
                requiresRedCard = false;
                return true;

            }
        }
        return false;
    }

    public void OpenDoor()
    {
        isTransitioning = true;
        doorAnimation.Play(nameOpenAnimation);
        PlayDoorOpen();
    }
    public void CloseDoor()
    {
        isTransitioning = true;
        doorAnimation.Play(nameCloseAnimation);
        PlayDoorClose();
    }
    void AnimationComplete()
    {
        isOpening = !isOpening;
        isTransitioning = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre en collision avec la porte
        PlayerRole playerScript = other.GetComponent<PlayerRole>();
        if (playerScript != null)
        {
            interactingPlayer = playerScript;
        }
        if (requiresAccessCard && AccessCardText != null)
        {
            AccessCardText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // R�initialise la r�f�rence au joueur lorsque le joueur quitte la zone de la porte
        PlayerRole playerScript = other.GetComponent<PlayerRole>();
        if (playerScript != null && playerScript == interactingPlayer)
        {
            interactingPlayer = null;
            if (AccessCardText != null)
            {
                AccessCardText.SetActive(false);
            }
        }
    }

    private void PlayDoorOpen()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(4, audioSource);
    }

    private void PlayDoorClose()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(5, audioSource);
    }
}

