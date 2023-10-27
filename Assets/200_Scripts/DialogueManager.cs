using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed = 0.02f;
    public float displayDuration = 2.0f; // Dur�e d'affichage de chaque ligne de dialogue
    public float interSentenceDelay = 1.0f; // D�lai entre les phrases

    private int index;
    private bool isTriggered = false;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(AnimateDialogue());
        }
    }

    IEnumerator AnimateDialogue()
    {
        foreach (string sentence in sentences)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            yield return new WaitForSeconds(displayDuration);
            textDisplay.gameObject.SetActive(false);
            yield return new WaitForSeconds(interSentenceDelay);
            textDisplay.gameObject.SetActive(true);
        }

        // Ajoute d'autres actions apr�s la fin du dialogue
        textDisplay.text = ""; // Assure que le texte est vide � la fin du dialogue
        textDisplay.gameObject.SetActive(false); // D�sactive compl�tement l'objet TextMeshProUGUI
    }

    IEnumerator TypeSentence(string sentence)
    {
        textDisplay.gameObject.SetActive(true); // Active l'objet TextMeshProUGUI avant de commencer � �crire
        textDisplay.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
