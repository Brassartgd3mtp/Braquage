using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGoal : InteractibleObjectV2
{
    [SerializeField] private Victory collectedGoalScript;

    private void Start()
    {
        // Obtenez la référence au script Victory associé à cet objet, l'objet doit être en parent comme component
        collectedGoalScript = GetComponentInParent<Victory>();
    }
    public override void OnInteraction(GameObject interactablePlayer)
    {
        if (collectedGoalScript != null)
        {
            collectedGoalScript.collectedGoal++;
        }
        Destroy(gameObject);
    }
}
