using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootableGain : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGain;

    void Start()
    {
        textGain = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textGain.text = $"+{BountyManager.Instance.bountyGain}";
    }
}
