using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVaultV2 : CrochetDoorV2
{
    public GameObject Text;
    private bool playerNearby = false;
    private void Start()
    {
        Text.gameObject.SetActive(false);
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
            // Si la porte est verrouillée et aucun crochetage n'est en cours, détecte le joueur le plus proche
            GetClosestPlayer();
        }
        else if (!isLocked)
        {
            base.OnInteraction(gameObject);
        }
    }

    // Méthode pour détecter le rôle du joueur le plus proche lorsque la porte est verrouillée et lui permet de percer le coffre
    private void GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            float closestDistance = float.MaxValue;
            GameObject closestPlayer = null;

            // Parcourt tous les joueurs pour trouver le plus proche
            foreach (var player in players)
            {
                Vector3 closestPoint = player.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Vérifie si le joueur est visible depuis la porte
                if (Physics.Raycast(transform.position, closestPoint - transform.position, out RaycastHit hit, raycastDistance, LayerMask.GetMask("Clickable")))
                {
                    if (hit.collider.CompareTag("Player") && hit.collider.gameObject == player && hit.distance < closestDistance)
                    {
                        closestDistance = hit.distance;
                        closestPlayer = player;
                    }
                }
            }

            if (closestPlayer != null)
            {
                PlayerRole playerRole = closestPlayer.GetComponent<PlayerRole>();

                // Vérifie le rôle du joueur (technicien) et déverrouille la porte si le joueur est autorisé
                if (playerRole != null && playerRole._technician)
                {
                    StartCoroutine(PickDoorVaultCoroutine());
                    barProgressionUI.AugmenterFillAmount();
                }
            }
        }
    }
    IEnumerator PickDoorVaultCoroutine()
    {
        isBeingPicked = true;
        sparkParticle.SetActive(true);
        foreach (PlayerRole roleScript in _playerRole)
        {
            totalPickingTime += roleScript._pickingMultiplier * pickingDuration;
        }
        yield return new WaitForSeconds(totalPickingTime);

        UnlockDoor();
        sparkParticle.SetActive(false);
        isBeingPicked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Text.gameObject.SetActive(true);
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Text.gameObject.SetActive(false);
        }
    }
}
