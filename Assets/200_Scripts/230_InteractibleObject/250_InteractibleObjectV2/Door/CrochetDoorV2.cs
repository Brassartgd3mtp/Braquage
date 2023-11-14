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
        // Vérifie si le collider est celui du joueur
        if (other.CompareTag("Player"))
        {
            // Ajoute le script du joueur à la liste s'il n'est pas déjà présent
            PlayerRole _role = other.GetComponent<PlayerRole>();
            if (_role != null && !_playerRole.Contains(_role))
            {
                _playerRole.Add(_role);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le collider est celui du joueur et le retire de la liste s'il est présent
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

        // Attends la durée nécessaire pour crocheter la porte en fonction de la dureté du crochetage et du rôle du joueur qui effectue l'action
        foreach (PlayerRole roleScript in _playerRole)
        {
            totalPickingTime += roleScript._pickingMultiplier * pickingDuration;
        }

        yield return new WaitForSeconds(totalPickingTime);

        UnlockDoor();
        isBeingPicked = false;
    }
}
