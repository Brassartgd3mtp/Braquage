using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAlert : MonoBehaviour
{
    private Image image;

    public Color _firstColor = new Color(1.0f, 0.0f, 0.0f, 0.5f); 
    public Color _secondColor = new Color(0.0f, 0.0f, 1.0f, 0.5f);

    public float _speedBlink = 0.2f;
    public int _numberBlink = 5;
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        int actualBlink = 0;

        while (actualBlink < _numberBlink)
        {
            // Change la couleur de l'image à rouge
            image.color = _firstColor;
            yield return new WaitForSeconds(_speedBlink);

            // Change la couleur de l'image à bleu
            image.color = _secondColor;
            yield return new WaitForSeconds(_speedBlink);

            actualBlink++;
        }

        gameObject.SetActive(false);
    }
}
