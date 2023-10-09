using UnityEngine;

public class ObjectVault : InteractibleObject
{
    [SerializeField]private bool isLocked = true;
    [SerializeField]private bool isBeingPicked = false;
    public float pickingDuration = 5.0f; // Durée nécessaire pour crocheter le coffre-fort
    private Collider coffreFortCollider;

    void Start()
    {
        coffreFortCollider = GetComponent<Collider>();
        LockVault();
    }


    
    public override void OnInteraction()
    {
        if (!isBeingPicked && isLocked)
        {
            StartCoroutine(PickVaultCoroutine());
        }
    }

    System.Collections.IEnumerator PickVaultCoroutine()
    {
        isBeingPicked = true;

        // Attends la durée nécessaire pour crocheter le coffre-fort
        yield return new WaitForSeconds(pickingDuration);

        // Code pour ouvrir le coffre-fort (peut être similaire à la transition de la porte)
        Debug.Log("Le coffre-fort a été crocheté et s'ouvre !");

        UnlockVault();
        isBeingPicked = false;
    }

    void UnlockVault()
    {
        isLocked = false;
        // Code pour déverrouiller le coffre-fort si nécessaire
        Debug.Log("Le coffre-fort est déverrouillé.");
    }

    void LockVault()
    {
        isLocked = true;
        // Code pour verrouiller le coffre-fort au départ si nécessaire
        Debug.Log("Le coffre-fort est verrouillé.");
    }
}
