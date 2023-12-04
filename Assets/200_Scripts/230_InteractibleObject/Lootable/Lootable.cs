using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    public enum CardType
    {
        RedCard,
        BlueCard,
        GreenCard,
        None
    }
    public CardType cardType;

    public int bounty;
}
