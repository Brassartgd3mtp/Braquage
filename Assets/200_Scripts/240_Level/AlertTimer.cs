using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertTimer : MonoBehaviour
{
    public float _initialTime = 120.0f; // Temps initial en secondes
    private float currentTime; // Temps actuel en secondes

    public bool _isTimerRunning = false; // Indique si le timer est en cours d'ex�cution

    public TextMeshProUGUI timerText; // Text pour afficher le timer

    public Defeat defeat;

    void Start()
    {
        currentTime = _initialTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (_isTimerRunning)
        {
            UpdateTimer();
            UpdateTimerText();
        }
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            // Appelle la m�thode lorsque le temps atteint z�ro
            TimeReachedZero();
            _isTimerRunning = false; // Arr�te le timer
        }
    }

    void UpdateTimerText()
    {
        // Met � jour le texte pour afficher le temps restant
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime);
        }
    }

    string FormatTime(float timeInSeconds)
    {
        // Convertit le temps en minutes et secondes
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Formate le temps en cha�ne de caract�res (mm:ss)
        return string.Format("Temps avant l'arriv�e de la police : {0:00}:{1:00}", minutes, seconds);
    }

    void TimeReachedZero()
    {
        defeat.DefeatGame();
        Debug.Log("Le temps est �coul�!");
    }
}
