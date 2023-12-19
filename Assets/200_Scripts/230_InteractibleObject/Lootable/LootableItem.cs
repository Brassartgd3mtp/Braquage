using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LootableItem : InteractibleObjectV2
{
    public LootableManager gameManager;
    public LootableBar lootableBar;
    public Animation lootableAnimation;
    public bool wasSearched = false;

    public GameObject childObject;


    public int lootTableID; // L'ID de la LootTable à utiliser
    public float totalSearchTime = 2f; //Temps de fouille
    public string nameAnimation;

    void Start()
    {
        gameManager = GetComponentInParent<LootableManager>();
        lootableAnimation = GetComponent<Animation>();

        if (gameManager != null)
        {
            // Attribuer un ID unique et aléatoire en fonction du nombre d'éléments dans la liste du GameManager
            lootTableID = gameManager.GenerateUniqueRandomID();
        }
        if (lootableAnimation == null)
        {
            Debug.LogError("Animation component is not assigned to ObjectDoorV2.");
            return;
        }
    }

    public override void OnInteraction(GameObject interactingPlayer)
    {
        if (gameManager != null)
        {
            LootTable selectedLootTable = gameManager.GetLootTableByID(lootTableID);

            if (selectedLootTable != null)
            {
                Debug.Log("Le joueur a interagi avec : " + selectedLootTable);

                // Modifier cette ligne pour trouver dynamiquement le PlayerRole en fonction du joueur qui interagit
                PlayerRole accesCard = FindPlayerRole(interactingPlayer);

                if (accesCard != null && !wasSearched)
                {
                    StartCoroutine(DelayedAction(accesCard, selectedLootTable));

                    // Désactiver le NavMeshAgent du joueur pendant la fouille
                    NavMeshAgent navMeshAgent = interactingPlayer.GetComponent<NavMeshAgent>();
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.speed = 0f;
                    }

                    lootableBar.AugmenterFillAmount();

                    // Récupérer l'Animator du joueur
                    Animator playerAnimator = interactingPlayer.GetComponent<Animator>();
                    playerAnimator.SetBool("DoingAction", true);
                    playerAnimator.SetBool("Walk", false);
                }
            }
            else
            {
                Debug.Log("Aucune LootTable trouvée avec l'ID : " + lootTableID);
            }
        }
        else
        {
            Debug.LogError("GameManager not found in the scene");
        }
    }
    IEnumerator DelayedAction(PlayerRole accesCard, LootTable selectedLootTable)
    {
        // Attendre 2 secondes
        yield return new WaitForSeconds(totalSearchTime);
        Animator playerAnimator = accesCard.GetComponent<Animator>();
        wasSearched = true;
        playerAnimator.SetBool("DoingAction", false);

        // Réactiver le NavMeshAgent du joueur après la fouille
        NavMeshAgent navMeshAgent = accesCard.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = 3.5f;
        }

        // Définir les booléens du joueur en fonction des paramètres de la carte
        switch (selectedLootTable.cardType)
        {
            case LootTable.CardType.RedCard:
                accesCard.RedCard = true;
                break;
            case LootTable.CardType.BlueCard:
                accesCard.blueCard = true;
                break;
            case LootTable.CardType.GreenCard:
                accesCard.greenCard = true;
                break;
            case LootTable.CardType.None:
                break;
            default:
                break;
        }


        BountyManager.Instance.AddBounty(selectedLootTable.bounty);
        lootableAnimation.Play(nameAnimation);
    }
    PlayerRole FindPlayerRole(GameObject interactingPlayer)
    {
        PlayerRole accesCard = null;

        // Utiliser le GameObject du joueur qui interagit
        if (interactingPlayer != null)
        {
            accesCard = interactingPlayer.GetComponent<PlayerRole>();
        }

        return accesCard;
    }

    void SearchCompleteATM()
    {
        Collider collider = GetComponent<Collider>();
        
        if (collider != null)
        {
            collider.enabled = false;
        }

        if (childObject != null)
        {
            // Récupérez le composant Animation du GameObject enfant
            Animation childAnimation = childObject.GetComponent<Animation>();

            // Assurez-vous que le composant Animation existe
            if (childAnimation != null)
            {
                // Jouez l'animation souhaitée
                childAnimation.Play();
                PlayPickUp();
            }
            else
            {
                Debug.LogError("Le GameObject enfant ne possède pas de composant Animation.");
            }
        }
        else
        {
            Debug.LogError("La référence au GameObject enfant n'est pas définie.");
        }
    }

    private void PlayPickUp()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(6, audioSource);
    }
}
