using UnityEngine;

public class ObjectVault : InteractibleObject
{
    [SerializeField]private bool isLocked = true;
    [SerializeField]private bool isBeingPicked = false;
    public float pickingDuration = 5.0f; // Dur�e n�cessaire pour crocheter le coffre-fort
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

        // Attends la dur�e n�cessaire pour crocheter le coffre-fort
        yield return new WaitForSeconds(pickingDuration);

        // Code pour ouvrir le coffre-fort (peut �tre similaire � la transition de la porte)
        Debug.Log("Le coffre-fort a �t� crochet� et s'ouvre !");

        UnlockVault();
        isBeingPicked = false;
    }

    void UnlockVault()
    {
        isLocked = false;
        // Code pour d�verrouiller le coffre-fort si n�cessaire
        Debug.Log("Le coffre-fort est d�verrouill�.");
    }

    void LockVault()
    {
        isLocked = true;
        // Code pour verrouiller le coffre-fort au d�part si n�cessaire
        Debug.Log("Le coffre-fort est verrouill�.");
    }
}
