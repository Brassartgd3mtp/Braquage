using UnityEngine;

public class Defeat : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDesactivate;

    public static Defeat Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void DefeatGame()
    {
        // Inverse l'état du temps (pause/resume)
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Résume le temps (joue)
        }
        else
        {
            Time.timeScale = 0f; // Met en pause le temps
        }

        // Active ou désactive le GameObject en fonction de l'état actuel du temps
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        if (objectToDesactivate != null)
        {
            objectToDesactivate.SetActive(false);
        }
    }
}
