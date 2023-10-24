using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    //Reference : https://www.youtube.com/watch?v=vAVi04mzeKk&ab_channel=SpawnCampGames
    [Header("List Unit")]
    // Liste de toutes les unités disponibles dans le jeu
    public List<GameObject> unitList = new List<GameObject>();
    // Liste des unités actuellement sélectionnées
    public List<GameObject> unitsSelected = new List<GameObject>();

    // Instance singleton pour s'assurer qu'une seule instance de ce script existe
    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    // Retourne le nombre total d'unités disponibles
    public int TotalCharacters()
    {
        return unitList.Count;
    }

    private void Awake()
    {
        //Si une instance existe et que ce n'est pas celle-ci
        if (_instance != null && _instance != this)
        {
            // On détruit cette instance
            Destroy(this.gameObject);
        }
        else
        {
            // On en fait l'instance
            _instance = this;
        }
    }

    // Sélectionne une unité et active ses composants de mouvement et d'interaction
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
        unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

        // Sélectionne l'unité pour des mises en évidence visuelles ou d'autres indicateurs
        Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

        if (unitOutline != null)
        {
            unitOutline.selectUnit = true;
        }
    }

    // Sélectionne ou désélectionne une unité en fonction de sa présence dans la sélection
    public void ShifClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            // Si l'unité n'est pas sélectionnée, l'ajouter à la sélection et activer ses composants
            unitsSelected.Add(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.selectUnit = true;
            }

        }
        else
        {
            // Si l'unité est déjà sélectionnée, la désélectionner et désactiver ses composants
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = false;
            unitsSelected.Remove(unitToAdd);

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.selectUnit = false;
            }
        }
    }

    // Ajoute une unité à la sélection pendant une opération de glisser
    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.selectUnit = true;
                
            }
        }
    }

    // Désélectionne toutes les unités actuellement sélectionnées et désactive leurs composants
    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<PlayerInteractions>().enabled = false;
            unit.GetComponent<UnitMovement>().enabled = false;

            Unit unitOutline = unit.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.selectUnit = false;
            }
        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {

    }
}
