using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance; // Instance unique pour accéder à partir d'autres scripts

    public TextMeshProUGUI bountyText; // Référence au TextMeshPro associé au bounty

    public int bountyGain;

    private int totalBounty = 0; // Valeur totale du bounty

    public int TotalBounty
    {
        get { return totalBounty; }
        set
        {
            totalBounty = value;
        }
    }

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
        TotalBounty += amount;
        bountyGain = amount;
        
        if (amount > 1)
        {
            PlaySFXBounty();
        }

        UpdateBountyText();
    }

    private void UpdateBountyText()
    {
        if (bountyText != null)
        {
            bountyText.text = "Total Bounty: " + TotalBounty.ToString();
        }
        else
        {
            Debug.LogError("Bounty Text not assigned in the inspector.");
        }
    }

    private void PlaySFXBounty()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(12, audioSource);
    }
}