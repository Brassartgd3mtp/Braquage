using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrochetDoorV2 : ObjectDoorV2
{
    private void Start()
    {
        doorAnimation = GetComponent<Animation>();
        if (doorAnimation == null)
        {
            Debug.LogError("Animation component is not assigned to ObjectDoorV2.");
            return;
        }

        LockDoor();
    }
    public override void OnInteraction(GameObject interactablePlayer)
    {
        if (!isBeingPicked && isLocked)
        {
            StartCoroutine(PickDoorCoroutine());
            barProgressionUI.AugmenterFillAmount();
        }
        else if (!isLocked)
        {
            base.OnInteraction(gameObject);
        }
        else
        {
            Debug.Log("GNNNNNNNNNNNNNNN");
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le collider est celui du joueur
        if (other.CompareTag("Player"))
        {
            // Ajoute le script du joueur � la liste s'il n'est pas d�j� pr�sent
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && !_playerRole.Contains(_role))
            {
                _playerRole.Add(_role);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le collider est celui du joueur et le retire de la liste s'il est pr�sent
        if (other.CompareTag("Player"))
        {
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && _playerRole.Contains(_role))
            {
                _playerRole.Remove(_role);
            }
        }
    }
    IEnumerator PickDoorCoroutine()
    {
        isBeingPicked = true;

        // Attends la dur�e n�cessaire pour crocheter la porte en fonction de la duret� du crochetage et du r�le du joueur qui effectue l'action
        foreach (PlayerRole roleScript in _playerRole)
        {
            totalPickingTime += roleScript._pickingMultiplier * pickingDuration;
        }

        yield return new WaitForSeconds(totalPickingTime);

        UnlockDoor();
        isBeingPicked = false;
    }
}
