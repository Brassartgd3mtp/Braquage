using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    //Reference : https://www.youtube.com/watch?v=vAVi04mzeKk&ab_channel=SpawnCampGames
    [Header("List Unit")]
    // Liste de toutes les unit�s disponibles dans le jeu
    public List<GameObject> unitList = new List<GameObject>();
    // Liste des unit�s actuellement s�lectionn�es
    public List<GameObject> unitsSelected = new List<GameObject>();

    // Instance singleton pour s'assurer qu'une seule instance de ce script existe
    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    // Retourne le nombre total d'unit�s disponibles
    public int TotalCharacters()
    {
        return unitList.Count;
    }

    private void Awake()
    {
        //Si une instance existe et que ce n'est pas celle-ci
        if (_instance != null && _instance != this)
        {
            // On d�truit cette instance
            Destroy(this.gameObject);
        }
        else
        {
            // On en fait l'instance
            _instance = this;
        }
    }

    // S�lectionne une unit� et active ses composants de mouvement et d'interaction
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
        unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

        // S�lectionne l'unit� pour des mises en �vidence visuelles ou d'autres indicateurs
        Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

        if (unitOutline != null)
        {
            unitOutline.selectUnit = true;
        }
    }

    // S�lectionne ou d�s�lectionne une unit� en fonction de sa pr�sence dans la s�lection
    public void ShifClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            // Si l'unit� n'est pas s�lectionn�e, l'ajouter � la s�lection et activer ses composants
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
            // Si l'unit� est d�j� s�lectionn�e, la d�s�lectionner et d�sactiver ses composants
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

    // Ajoute une unit� � la s�lection pendant une op�ration de glisser
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

    // D�s�lectionne toutes les unit�s actuellement s�lectionn�es et d�sactive leurs composants
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
