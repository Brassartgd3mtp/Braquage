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

        UnlockVault();
        isBeingPicked = false;
    }

    void UnlockVault()
    {
        isLocked = false;
    }

    void LockVault()
    {
        isLocked = true;
    }
}
