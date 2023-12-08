// Ce script gère la victoire du joueur en vérifiant si tous les objectifs ont été accomplis.
// Il inclut la gestion de la fenêtre de victoire, la pause du jeu et le suivi des objectifs et personnages dans une zone spécifique.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory : MonoBehaviour
{
    public GameObject victoryWindow;     // Référence à la fenêtre de victoire dans l'éditeur Unity
    public ActivePause activePause;      // Référence au script ActivePause pour gérer la pause du jeu
    public TextMeshProUGUI goalCountText; // Texte affichant le nombre d'objectifs accomplis
    public TextMeshProUGUI charactersInZoneText;  // Texte affichant le nombre de personnages dans la zone

    public int totalCharactersNeed;      // Nombre total de personnages nécessaires pour la victoire
    [SerializeField] private int charactersInZone = 0;  // Nombre de personnages actuellement dans la zone

    public static List<GameObject> goalObjects = new List<GameObject>();  // Liste statique d'objectifs
    public int collectedGoal = 0;       // Nombre d'objectifs accomplis
    public bool objectiveCompleted = false; // Variable indiquant si l'objectif est accompli

    public GameObject zoneInactive;
    public GameObject zoneActive;

    private void Start()
    {
        // Attend une seconde avant de vérifier le nombre total de personnages
        Invoke("CheckTotalCharacters", 1f);
        PopulateGoalList();
    }

    private void Update()
    {
        CheckTotalGoal();

        // Affiche le nombre de personnages dans la zone si le texte existe et l'objectif est accompli
        if (charactersInZoneText != null && objectiveCompleted)
        {
            charactersInZoneText.gameObject.SetActive(true);
            charactersInZoneText.text = "Characters In Zone: " + charactersInZone + "/" + totalCharactersNeed;

            zoneActive.SetActive(true);
            zoneInactive.SetActive(false);
        }
    }

    #region CheckTotalCharacters - Vérifie que tout les personnages sont dans la zone de fin pour l'extraction
    // Vérifie que tout les personnages sont dans la zone de fin pour l'extraction
    private void CheckTotalCharacters()
    {
        int unitListCount = UnitSelections.Instance.TotalCharacters();

        totalCharactersNeed = unitListCount;
    }

    // Appelée lorsque quelque chose entre dans la zone de victoire
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInZone++;

            // Vérifie si tous les personnages sont dans la zone
            if (charactersInZone == totalCharactersNeed && objectiveCompleted)
            {
                ShowVictoryWindow();
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

    // Affiche la fenêtre de victoire
    private void ShowVictoryWindow()
    {
        if (victoryWindow != null)
        {
            victoryWindow.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("Victory window is not assigned!");
        }
    }
    #endregion


    #region CheckTotalGoal - Vérifie que tout les objectifs ont été récupérés pour l'extraction

    // Vérifie que tout les objectifs ont été récupérés pour l'extraction
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

    // Initialise la liste des objectifs en trouvant tous les GameObjects avec le tag "Goal"
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
