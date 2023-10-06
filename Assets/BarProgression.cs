using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarProgression : MonoBehaviour
{
    public Image progression;
    public CrochetDoor crochetDoor;
    public GameObject destroyHimSelf;

    public Color startColor;
    public Color endColor;

    public void AugmenterFillAmount()
    {
        if (progression != null && crochetDoor != null)
        {
            StartCoroutine(IncreaseFillAmountOverTime(crochetDoor._pickingDuration));
        }
        else
        {
            Debug.LogError("L'objet Image ou CrochetDoor n'est pas correctement assigné.");
        }
    }
    private IEnumerator IncreaseFillAmountOverTime(float duration)
    {
        float timer = 0f;
        float initialFillAmount = progression.fillAmount;
        float targetFillAmount = Mathf.Clamp01(initialFillAmount + 1);

        Color initialColor = progression.color;


        while (timer < duration)
        {
            timer += Time.deltaTime;

            // Interpolation linéaire pour la valeur du fillAmount
            progression.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, timer / duration);

            // Interpolation linéaire pour la couleur
            progression.color = Color.Lerp(startColor, endColor, timer / duration);

            yield return null;
        }

        progression.fillAmount = targetFillAmount; // Assure la valeur finale
        progression.color = endColor; // Assure la couleur finale
        Destroy(destroyHimSelf);
    }
}
