using System.Collections.Generic;
using UnityEngine;

public class LootableItem : InteractibleObjectV2
{
    public int lootTableID; // L'ID de la LootTable � utiliser
    private LootableManager gameManager;

    void Start()
    {
        gameManager = GetComponentInParent<LootableManager>();
        if (gameManager != null)
        {
            // Attribuer un ID unique et al�atoire en fonction du nombre d'�l�ments dans la liste du GameManager
            lootTableID = gameManager.GenerateUniqueRandomID();
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

                if (accesCard != null)
                {
                    // D�finir les bool�ens du joueur en fonction des param�tres de la carte
                    switch (selectedLootTable.cardType)
                    {
                        case LootTable.CardType.RedCard:
                            accesCard.redCard = true;
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

                    Debug.Log("SetActive False");
                    // D�sactiver l'objet carte apr�s utilisation
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Aucune LootTable trouv�e avec l'ID : " + lootTableID);
            }
        }
        else
        {
            Debug.LogError("GameManager not found in the scene");
        }
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
}
