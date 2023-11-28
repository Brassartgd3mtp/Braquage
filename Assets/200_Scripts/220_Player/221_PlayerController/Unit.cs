using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public bool selectUnit = false;
    private new Renderer renderer;

    //Nom des références exact sur lesquels on veut agir dans le script
    [Header("Name property Shader")] 
    public string nameMaterialOutline = "M_OutlineV2";
    public string propertyAlpha = "_Alpha";
    public string propertyColor = "_Outline_Color";

    // Remplace avec la nouvelle ou l'ancienne valeur d'alpha
    [Header("Alpha property")]
    public float originAlpha = 0f;
    public float newAlpha = 1f;

    [Header("Color property")] //Permet de définir les couleurs et l'intensité lorsque la souris passe sur objet et qu'un "Player" est à proximité
    public Color selectionColor;
    [Range(-10.0f, 100.0f)]
    public float intensityHDR = 10f;

    [Header("UI property")]
    public GameObject outlineUI;
    public GameObject actionPercingUI;
    public GameObject actionWithBagUI;
    public GameObject actionLockPickingUI;

    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
        renderer = GetComponent<Renderer>();
        ApplyAlpha(originAlpha);
        ApplyColor(selectionColor, intensityHDR);
    }

    private void Update()
    {
        if (selectUnit)
        {
            ApplyAlpha(newAlpha);
            outlineUI.SetActive(true);
        }
        else if (!selectUnit)
        {
            ApplyAlpha(originAlpha);
            outlineUI.SetActive(false);
        }
    }

    // Méthode pour appliquer l'alpha au nameMaterialOutline
    public void ApplyAlpha(float alpha)
    {
        // Parcourt tous les matériaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // Vérifie si la propriété Alpha existe dans ce matériau
            if (material.HasProperty(propertyAlpha))
            {
                // Modifie la propriété Alpha spécifique à ce matériau
                material.SetFloat(propertyAlpha, alpha);
            }
        }
    }

    // Méthode pour appliquer la couleur au nameMaterialOutline
    public void ApplyColor(Color couleur, float intensite)
    {
        // Parcourt tous les matériaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // Vérifie si la propriété Couleur existe dans ce matériau
            if (material.HasProperty(propertyColor))
            {
                // Modifie la propriété Couleur spécifique à ce matériau
                material.SetColor(propertyColor, couleur * intensite);
            }
        }
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }
}
