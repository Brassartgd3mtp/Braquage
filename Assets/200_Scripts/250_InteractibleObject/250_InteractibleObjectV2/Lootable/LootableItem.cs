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

    //public void OnTriggerEnter(Collider other)
             //public override void OnInteraction()
        void OnTriggerEnter (Collider other)
    {
        if (gameManager != null)
        {
            LootTable selectedLootTable = gameManager.GetLootTableByID(lootTableID);

            if (selectedLootTable != null)
            {
                Debug.Log("Le joueur a interagi avec : " + selectedLootTable);
                // Fais ce que tu veux avec les donn�es de l'objet ici

                PlayerRole accesCard = other.GetComponent<PlayerRole>();
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
}
