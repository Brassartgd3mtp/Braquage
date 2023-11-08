using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootableManager : MonoBehaviour
{
    public List<LootTable> lootTables = new List<LootTable>();
    private HashSet<int> usedIDs = new HashSet<int>();



    void Start()
    {

    }

    public LootTable GetLootTableByID(int id)
    {
        if (id >= 0 && id < lootTables.Count)
        {
            LootTable selectedLootTable = lootTables[id];
            return selectedLootTable;
        }
        else
        {
            Debug.LogError("Invalid LootTable ID");
            return null;
        }
    }

    public int GenerateUniqueRandomID()
    {
        int maxID = lootTables.Count;

        // Générer un ID aléatoire jusqu'à ce qu'il soit unique
        int randomID;
        do
        {
            randomID = Random.Range(0, maxID);
        } while (usedIDs.Contains(randomID));

        // Ajouter l'ID à la HashSet
        usedIDs.Add(randomID);

        return randomID;
    }
}
