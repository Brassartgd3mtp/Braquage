// Script enfant : SpecificInteractibleObject.cs

using UnityEngine;

public class SpecificInteractibleObject : InteractibleObject
{
    //Faire une m�thode pour d�sactiver le trigger pour les coffres et crochetage
    public override void OnInteraction()
    {
        base.OnInteraction(); // Appelle d'abord la m�thode du script parent si n�cessaire

        Debug.Log("Contact Enfant");
        // Ajoute le code sp�cifique � cet objet interactif
    }
}