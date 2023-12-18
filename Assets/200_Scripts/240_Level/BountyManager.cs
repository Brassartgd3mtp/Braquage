using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance; // Instance unique pour accéder à partir d'autres scripts

    public TextMeshProUGUI bountyText; // Référence au TextMeshPro associé au bounty

    public int bountyGain;

    private int totalBounty = 0; // Valeur totale du bounty

    private void Start()
    {
        bountyText = GetComponentInChildren<TextMeshProUGUI>();
    }

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
        bountyGain = amount;
        UpdateBountyText();
    }

    private void UpdateBountyText()
    {
        if (bountyText != null)
        {
            bountyText.text = "Total Bounty: " + totalBounty.ToString();
        }
        else
        {
            Debug.LogError("Bounty Text not assigned in the inspector.");
        }
    }
}