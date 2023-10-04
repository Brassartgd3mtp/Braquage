using System.Collections;
using System.Collections.Generic;
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
        LockCoffreFort();
    }


    
    public override void OnInteraction()
    {
        if (!isBeingPicked && isLocked)
        {
            StartCoroutine(PickCoffreFortCoroutine());
        }
    }

    System.Collections.IEnumerator PickCoffreFortCoroutine()
    {
        isBeingPicked = true;

        // Attends la dur�e n�cessaire pour crocheter le coffre-fort
        yield return new WaitForSeconds(pickingDuration);

        // Code pour ouvrir le coffre-fort (peut �tre similaire � la transition de la porte)
        Debug.Log("Le coffre-fort a �t� crochet� et s'ouvre !");

        UnlockCoffreFort();
        isBeingPicked = false;
    }

    void UnlockCoffreFort()
    {
        isLocked = false;
        // Code pour d�verrouiller le coffre-fort si n�cessaire
        Debug.Log("Le coffre-fort est d�verrouill�.");
    }

    void LockCoffreFort()
    {
        isLocked = true;
        // Code pour verrouiller le coffre-fort au d�part si n�cessaire
        Debug.Log("Le coffre-fort est verrouill�.");
    }
}
