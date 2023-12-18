using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectGoal : InteractibleObjectV2
{
    [SerializeField] private Victory collectedGoalScript;
    [SerializeField] private PlayerRole interactingPlayer;

    public float fadeTime = 1f;
    [SerializeField] private TextMeshProUGUI noLootable;
    public float displayTime = 1f;

    private void Start()
    {
        collectedGoalScript = GetComponentInParent<Victory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerRole playerScript = other.GetComponent<PlayerRole>();
        if (playerScript != null)
        {
            interactingPlayer = playerScript;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerRole playerScript = other.GetComponent<PlayerRole>();
        if (playerScript != null && playerScript == interactingPlayer)
        {
            interactingPlayer = null;
        }
    }

    public override void OnInteraction(GameObject interactablePlayer)
    {
        if (collectedGoalScript != null && interactingPlayer.TakeGoal)
        {
            StartCoroutine(DisplayAndFade());
        }
        if (collectedGoalScript != null && !interactingPlayer.TakeGoal)
        {
            collectedGoalScript.collectedGoal++;
            interactingPlayer.TakeGoal = true;
            Destroy(gameObject);
        }
    }

    IEnumerator DisplayAndFade()
    {
        noLootable.gameObject.SetActive(true);

        // Afficher le texte
        noLootable.alpha = 1f;

        // Attendre pendant le temps d'affichage
        yield return new WaitForSeconds(displayTime);

        // Commencer à faire disparaître le texte progressivement
        float timer = 0f;
        while (timer < fadeTime)
        {
            // Calculer l'alpha en fonction du temps
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);

            // Appliquer l'alpha au composant TextMeshPro
            noLootable.alpha = alpha;

            // Mettre à jour le timer
            timer += Time.deltaTime;

            yield return null;
        }

        noLootable.gameObject.SetActive(false);
    }
}
