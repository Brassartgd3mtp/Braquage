using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGoal : InteractibleObject
{
    [SerializeField] private Victory collectedGoalScript;

    private void Start()
    {
        // Obtenez la référence au script Victory associé à cet objet, l'objet doit être en parent comme component
        collectedGoalScript = GetComponentInParent<Victory>();
    }
    public override void OnInteraction()
    {
        Debug.Log("Le goal interagit");
        if (collectedGoalScript != null)
        {
            collectedGoalScript.collectedGoal++;
        }
        Destroy(gameObject);
    }
}
