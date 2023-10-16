using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Script qui gère l'alerte et donc l'échec, l'activation de la sortie quand l'objectif est remplie et la réussite

    public static List<GameObject> goalObjects = new List<GameObject>();


    void Start()
    {
        PopulateGoalList();
    }

    void PopulateGoalList()
    {
        GameObject[] goalList = GameObject.FindGameObjectsWithTag("Goal");

        foreach (GameObject goalObject in goalList)
        {
            goalObjects.Add(goalObject);
        }

        Debug.Log("Number of goal objects in the list: " + goalObjects.Count);
    }

    void OnDestroy()
    {
        ClearGoalList();
    }

    void ClearGoalList()
    {
        goalObjects.Clear();
        Debug.Log("Goal list cleared.");
    }
}
