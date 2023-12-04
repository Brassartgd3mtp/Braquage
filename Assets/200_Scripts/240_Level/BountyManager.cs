using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance; // Instance unique pour accéder à partir d'autres scripts

    public List<TextMeshProUGUI> bountyTexts; // Liste des TextMeshPro associés au bounty

    public int totalBounty = 0; // Valeur totale du bounty

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // S'assurer qu'il n'y a qu'une seule instance
        }
    }

    public void AddBounty(int amount)
    {
        totalBounty += amount;
        UpdateBountyTexts();
    }

    private void UpdateBountyTexts()
    {
        foreach (TextMeshProUGUI text in bountyTexts)
        {
            text.text = "Total Bounty: " + totalBounty.ToString();
        }
    }
}
