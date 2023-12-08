using System.Collections.Generic;
using UnityEngine;

public class ObjectDoorV2 : InteractibleObjectV2
{
    public bool isOpening = false;
    private bool isTransitioning = false;
    private PlayerRole interactingPlayer;

    [Header("Spécifique au Door simple")]
    // Couleurs de carte d'accès nécessaires
    public bool requiresAccessCard = false;
    public bool requiresRedCard = false;
    public bool requiresBlueCard = false;
    public bool requiresGreenCard = false;

    [Header("Nom animation")]
    public string nameOpenAnimation;
    public string nameCloseAnimation;
    public string nameCardAnimation;


    [HideInInspector] public bool isLocked = true; //Bool si la porte est verrouillée
    [HideInInspector] public bool isBeingPicked = false; //Bool si le crochetage est effectué
    [HideInInspector] public List<PlayerRole> _playerRole = new List<PlayerRole>(); //Récupère le script de rôle des personnage pour en récupérer le _pickingMultiplier

    [Header("Difficulté en seconde")]
    public float pickingDuration = 5.0f; // Durée nécessaire pour crocheter la porte, ça définit la difficulté du crochetage
    [HideInInspector] public float totalPickingTime; //Durée total du crochetage en fonction du role du player

    [Header("GameObject")]
    public BarProgressionV2 barProgressionUI; //Récupère la barre d'UI qui permet de voir où en est le crochetage
    public GameObject sparkParticle;

    [HideInInspector] public float raycastDistance = 10f; // Distance du raycast pour détecter les joueurs

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
        // Vérifie si le joueur qui interagit avec la porte détient la carte d'accès appropriée
        if (interactingPlayer != null)
        {
            // Vérifie si le joueur a la carte
            if ((!requiresRedCard || (interactingPlayer.RedCard))
                && (!requiresBlueCard || (interactingPlayer.blueCard))
                && (!requiresGreenCard || (interactingPlayer.greenCard)))
            {
                // Marque la carte comme utilisée une fois qu'elle a été utilisée
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
        // Vérifie si le joueur entre en collision avec la porte
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
        // Réinitialise la référence au joueur lorsque le joueur quitte la zone de la porte
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

