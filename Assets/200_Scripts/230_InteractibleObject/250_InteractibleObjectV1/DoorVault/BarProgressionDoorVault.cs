using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarProgressionDoorVault : MonoBehaviour
{
    public Image progression;
    public DoorVault doorVault;
    public GameObject destroyHimSelf;

    public Color startColor = new Color(1.0f, 0.0f, 0.0f, 1.0f); // Rouge avec alpha � 1.0
    public Color endColor = new Color(0.0f, 1.0f, 0.0f, 1.0f); // Vert avec alpha � 1.0




    public void AugmenterFillAmount()
    {
        if (progression != null && doorVault != null)
        {
            StartCoroutine(IncreaseFillAmountOverTime(doorVault.pickingDuration));
        }
        else
        {
            Debug.LogError("L'objet Image ou CrochetDoor n'est pas correctement assign�.");
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

            // Interpolation lin�aire pour la valeur du fillAmount
            progression.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, timer / duration);

            // Interpolation lin�aire pour la couleur
            progression.color = Color.Lerp(startColor, endColor, timer / duration);

            yield return null;
        }

        progression.fillAmount = targetFillAmount; // Assure la valeur finale
        progression.color = endColor; // Assure la couleur finale
        Destroy(destroyHimSelf);
    }
}
