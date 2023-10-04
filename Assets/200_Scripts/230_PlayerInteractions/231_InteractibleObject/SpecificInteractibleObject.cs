// Script enfant : SpecificInteractibleObject.cs

using UnityEngine;

public class SpecificInteractibleObject : InteractibleObject
{
    //Faire une méthode pour désactiver le trigger pour les coffres et crochetage
    public override void OnInteraction()
    {
        base.OnInteraction(); // Appelle d'abord la méthode du script parent si nécessaire

        Debug.Log("Contact Enfant");
        // Ajoute le code spécifique à cet objet interactif
    }
}