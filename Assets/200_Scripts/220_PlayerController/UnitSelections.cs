using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    //Reference : https://www.youtube.com/watch?v=vAVi04mzeKk&ab_channel=SpawnCampGames
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    public Material newMaterial;
    public string materialToRemoveName; // Le nom du matériau spécifique que tu veux enlever



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

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        //unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
        unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

        Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

        if (unitOutline != null)
        {
            unitOutline.AddMaterial(newMaterial);
        }
    }

    public void ShifClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            //unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.AddMaterial(newMaterial);
            }

        }
        else
        {
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = false;
            //unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.RemoveMaterial(materialToRemoveName);
            }
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            //unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.GetComponent<PlayerInteractions>().enabled = true;

            Unit unitOutline = unitToAdd.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.AddMaterial(newMaterial);
            }

        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<PlayerInteractions>().enabled = false;
            unit.GetComponent<UnitMovement>().enabled = false;
            //unit.transform.GetChild(0).gameObject.SetActive(false);

            Unit unitOutline = unit.GetComponentInParent<Unit>();

            if (unitOutline != null)
            {
                unitOutline.RemoveMaterial(materialToRemoveName);
            }

        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {

    }
}
