using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlowdown : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownDuration = 2f;
    public int maxSlowdowns = 3;

    public TextMeshProUGUI slowdownText;

    private bool isSlowingDown = false;
    private float currentSlowdownAmount = 1f; // Initialiser à 1, représentant 100% du bullet time

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isSlowingDown && currentSlowdownAmount > 0)
            {
                StartCoroutine(SlowTime());
            }
            else
            {
                ResetTime();
            }
        }

        // Mettre à jour la jauge de bullet time dans l'UI
        slowdownText.text = $"Bullet Time : {(int)(currentSlowdownAmount * 100)}%";
    }

    private IEnumerator SlowTime()
    {
        isSlowingDown = true;

        float originalTimeScale = Time.timeScale;

        // Ralentir le temps en ajustant Time.timeScale (pour d'autres scripts)
        while (Time.timeScale > slowdownFactor)
        {
            Time.timeScale -= (1f / slowdownDuration) * Time.unscaledDeltaTime;
            currentSlowdownAmount = Mathf.Clamp01(Time.timeScale / originalTimeScale);
            yield return null;
        }

        // Assurez-vous que le temps est correctement réglé à la fin de la coroutine
        Time.timeScale = slowdownFactor;
        currentSlowdownAmount = slowdownFactor;
        ResetTime();
    }

    private void ResetTime()
    {
        // Rétablissez les valeurs originales
        Time.timeScale = 1f;
        currentSlowdownAmount = 1f;

        isSlowingDown = false;

    }
}