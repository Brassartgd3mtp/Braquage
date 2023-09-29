using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public string selectableTag = "Player"; // Tag des personnages s�lectionnables
    public List<Transform> selectedCharacters = new List<Transform>(); // Liste des personnages s�lectionn�s

    public List<string> exclusionTags = new List<string>(); // Tags des personnages � exclure de la s�lection

    public MonoBehaviour attachedScript; // Script attach� � appeler lors du clic sur le terrain

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Si le bouton gauche de la souris est enfonc�
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Effectue un raycast pour d�tecter les collisions avec le terrain
            if (Physics.Raycast(ray, out hit))
            {
                // Si la touche LeftShift est enfonc�e
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // Toggle la s�lection du personnage au clic
                    ToggleCharacterSelection(hit.collider.gameObject.transform);
                }
                else
                {
                    // S�lectionne le personnage au clic
                    SelectCharacter(hit.collider.gameObject.transform);
                }
            }
        }
    }

    private void ToggleCharacterSelection(Transform character)
    {
        // V�rifie si le personnage est d�j� s�lectionn�
        if (selectedCharacters.Contains(character))
        {
            // D�s�lectionne le personnage
            selectedCharacters.Remove(character);
        }
        else
        {
            // V�rifie si le personnage est dans la liste d'exclusion
            if (!IsExcluded(character))
            {
                // S�lectionne le personnage
                selectedCharacters.Add(character);
            }
        }
    }

    private void SelectCharacter(Transform character)
    {
        // D�s�lectionne tous les personnages actuellement s�lectionn�s
        ClearSelection();

        // V�rifie si le personnage est dans la liste d'exclusion
        if (!IsExcluded(character))
        {
            // S�lectionne le personnage au clic
            selectedCharacters.Add(character);

            // Appelle le script attach� si disponible
            CallAttachedScript();
        }
    }

    private void ClearSelection()
    {
        // D�s�lectionne tous les personnages
        selectedCharacters.Clear();
    }

    private bool IsExcluded(Transform character)
    {
        // V�rifie si le tag du personnage est dans la liste d'exclusion
        return exclusionTags.Contains(character.tag);
    }

    private void CallAttachedScript()
    {
        // V�rifie si un script est attach�
        if (attachedScript != null)
        {
            // Appelle la fonction du script attach�
            attachedScript.SendMessage("OnCharacterSelection", selectedCharacters);
        }
    }
}
