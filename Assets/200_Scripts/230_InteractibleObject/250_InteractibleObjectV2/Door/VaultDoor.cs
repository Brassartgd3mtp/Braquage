using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorVault : CrochetDoorV2
{
    /*
    public GameObject Text;
    private bool playerNearby = false;
    */

    public TextMeshProUGUI textRequiredRole;
    public float displayTime = 2f;
    public float fadeTime = 1f;

    private void Start()
    {
        //Text.gameObject.SetActive(false);
        textRequiredRole.gameObject.SetActive(false);

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
            // Si la porte est verrouill�e et aucun crochetage n'est en cours, d�tecte le joueur le plus proche
            GetClosestPlayer();
        }
        else if (!isLocked)
        {
            base.OnInteraction(gameObject);
        }
    }

    // M�thode pour d�tecter le r�le du joueur le plus proche lorsque la porte est verrouill�e et lui permet de percer le coffre
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

                // V�rifie si le joueur est visible depuis la porte
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

                // V�rifie le r�le du joueur (technicien) et d�verrouille la porte si le joueur est autoris�
                if (playerRole != null && playerRole._technician)
                {
                    StartCoroutine(PickDoorVaultCoroutine());
                    barProgressionUI.AugmenterFillAmount();
                }
                else if (playerRole != null && !playerRole._technician)
                {
                    StartCoroutine(DisplayAndFade());
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

    IEnumerator DisplayAndFade()
    {
        textRequiredRole.gameObject.SetActive(true);

        // Afficher le texte
        textRequiredRole.alpha = 1f;

        // Attendre pendant le temps d'affichage
        yield return new WaitForSeconds(displayTime);

        // Commencer � faire dispara�tre le texte progressivement
        float timer = 0f;
        while (timer < fadeTime)
        {
            // Calculer l'alpha en fonction du temps
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);

            // Appliquer l'alpha au composant TextMeshPro
            textRequiredRole.alpha = alpha;

            // Mettre � jour le timer
            timer += Time.deltaTime;

            yield return null;
        }

        textRequiredRole.gameObject.SetActive(false);
    }


    /*
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
    */
}
