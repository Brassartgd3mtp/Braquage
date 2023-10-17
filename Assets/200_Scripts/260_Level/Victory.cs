using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory : MonoBehaviour
{
    public GameObject victoryWindow;
    public ActivePause activePause;
    public TextMeshProUGUI goalCountText;
    public TextMeshProUGUI charactersInZoneText;


    public int totalCharactersNeed;
    [SerializeField] private int charactersInZone = 0;

    public static List<GameObject> goalObjects = new List<GameObject>();
    public int collectedGoal = 0;
    public bool objectiveCompleted = false; // Nouvelle variable pour l'objectif

    private void Start()
    {
        Invoke("CheckTotalCharacters", 1f);
        PopulateGoalList();
    }

    private void Update()
    {
        CheckTotalGoal();

        if (charactersInZoneText != null && objectiveCompleted)
        {
            charactersInZoneText.gameObject.SetActive(true);
            charactersInZoneText.text = "Characters In Zone: " + charactersInZone + "/" + totalCharactersNeed;
        }
    }

    #region CheckTotalCharacters - Vérifie que tout les personnages sont dans la zone de fin pour l'extraction
    private void CheckTotalCharacters()
    {
        int unitListCount = UnitSelections.Instance.TotalCharacters();

        totalCharactersNeed = unitListCount;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInZone++;

            // Vérifie si tous les personnages sont dans la zone
            if (charactersInZone == totalCharactersNeed && objectiveCompleted)
            {
                ShowVictoryWindow();
                PauseGame();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInZone--;
        }
    }

    private void ShowVictoryWindow()
    {
        if (victoryWindow != null)
        {
            victoryWindow.SetActive(true);
            // Ajouter ici d'autres actions liées à l'affichage de la fenêtre de victoire
        }
        else
        {
            Debug.LogError("Victory window is not assigned!");
        }
    }

    private void PauseGame()
    {
        if (activePause != null)
        {
            activePause.PauseGame();
        }
        else
        {
            Debug.LogError("PauseManager script is not assigned!");
        }
    }
    #endregion


    #region CheckTotalGoal - Vérifie que tout les objectifs ont été récupérés pour l'extraction
    private void CheckTotalGoal()
    {
        int goalListCount = goalObjects.Count;

        if (goalListCount == collectedGoal)
        {
            objectiveCompleted = true;

        }
        if (goalCountText != null)
        {
            goalCountText.text = "Goal Count: " + collectedGoal + "/" + goalListCount;
        }
    }

    void PopulateGoalList()
    {
        GameObject[] goalList = GameObject.FindGameObjectsWithTag("Goal");

        foreach (GameObject goalObject in goalList)
        {
            goalObjects.Add(goalObject);
        }
    }

    void OnDestroy()
    {
        ClearGoalList();
    }

    void ClearGoalList()
    {
        goalObjects.Clear();
    }
    #endregion
}
