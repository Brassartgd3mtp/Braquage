using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TestOutlineObject : MonoBehaviour
{
    public Material newMaterial; // Assure-toi de d�finir ce mat�riau dans l'Inspector
    public string materialToRemoveName = "NomDuMaterialAEnlever"; // Remplace cela par le nom du mat�riau que tu veux enlever

    public Material proximityMaterial; // Assure-toi de d�finir ce mat�riau dans l'Inspector
    public string proximityMaterialToRemoveName = "NomDuMaterialAEnlever"; // Remplace cela par le nom du mat�riau que tu veux enlever



    private bool playerNearby = false;

    void OnMouseEnter()
    {
        if (!playerNearby)
        {
            AddMaterial(newMaterial);
        }
    }

    void OnMouseExit()
    {
        // Ajoute la condition pour v�rifier si un joueur n'est pas � proximit�
        if (!playerNearby)
        {
            RemoveMaterial(materialToRemoveName);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            RemoveMaterial(materialToRemoveName);
            AddMaterial(proximityMaterial);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveMaterial(proximityMaterialToRemoveName);
            playerNearby = false;
        }
    }

    public void AddMaterial(Material material)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // R�cup�re les mat�riaux actuels
            List<Material> materialList = new List<Material>(meshRenderer.materials);

            // Ajoute le nouveau mat�riau � la liste des mat�riaux
            materialList.Add(material);

            // Applique la nouvelle liste de mat�riaux au MeshRenderer
            meshRenderer.materials = materialList.ToArray();
        }
    }

    public void RemoveMaterial(string materialName)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // R�cup�re les mat�riaux actuels
            List<Material> materialList = new List<Material>(meshRenderer.materials);

            // Recherche et enl�ve le mat�riau sp�cifi� de la liste par nom
            Material materialToRemove = materialList.Find(m => m.name == materialName);

            if (materialToRemove != null)
            {
                materialList.Remove(materialToRemove);

                // Applique la nouvelle liste de mat�riaux au MeshRenderer
                meshRenderer.materials = materialList.ToArray();
            }
        }
    }
}
