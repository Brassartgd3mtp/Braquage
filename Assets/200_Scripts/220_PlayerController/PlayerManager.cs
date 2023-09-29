using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public string selectableTag = "Player"; // Tag des personnages sélectionnables
    public List<Transform> selectedCharacters = new List<Transform>(); // Liste des personnages sélectionnés

    public List<string> exclusionTags = new List<string>(); // Tags des personnages à exclure de la sélection

    public MonoBehaviour attachedScript; // Script attaché à appeler lors du clic sur le terrain

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Effectue un raycast pour détecter les collisions avec le terrain
            if (Physics.Raycast(ray, out hit))
            {
                // Si la touche LeftShift est enfoncée
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // Toggle la sélection du personnage au clic
                    ToggleCharacterSelection(hit.collider.gameObject.transform);
                }
                else
                {
                    // Sélectionne le personnage au clic
                    SelectCharacter(hit.collider.gameObject.transform);
                }
            }
        }
    }

    private void ToggleCharacterSelection(Transform character)
    {
        // Vérifie si le personnage est déjà sélectionné
        if (selectedCharacters.Contains(character))
        {
            // Désélectionne le personnage
            selectedCharacters.Remove(character);
        }
        else
        {
            // Vérifie si le personnage est dans la liste d'exclusion
            if (!IsExcluded(character))
            {
                // Sélectionne le personnage
                selectedCharacters.Add(character);
            }
        }
    }

    private void SelectCharacter(Transform character)
    {
        // Désélectionne tous les personnages actuellement sélectionnés
        ClearSelection();

        // Vérifie si le personnage est dans la liste d'exclusion
        if (!IsExcluded(character))
        {
            // Sélectionne le personnage au clic
            selectedCharacters.Add(character);

            // Appelle le script attaché si disponible
            CallAttachedScript();
        }
    }

    private void ClearSelection()
    {
        // Désélectionne tous les personnages
        selectedCharacters.Clear();
    }

    private bool IsExcluded(Transform character)
    {
        // Vérifie si le tag du personnage est dans la liste d'exclusion
        return exclusionTags.Contains(character.tag);
    }

    private void CallAttachedScript()
    {
        // Vérifie si un script est attaché
        if (attachedScript != null)
        {
            // Appelle la fonction du script attaché
            attachedScript.SendMessage("OnCharacterSelection", selectedCharacters);
        }
    }
}
