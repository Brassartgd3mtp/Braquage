using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGoal : InteractibleObject
{
    [SerializeField] private Victory collectedGoalScript;

    private void Start()
    {
        // Obtenez la r�f�rence au script Victory associ� � cet objet, l'objet doit �tre en parent comme component
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
