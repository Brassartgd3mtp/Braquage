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
        // Inverse l'�tat du temps (pause/resume)
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // R�sume le temps (joue)
        }
        else
        {
            Time.timeScale = 0f; // Met en pause le temps
        }

        // Active ou d�sactive le GameObject en fonction de l'�tat actuel du temps
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
