using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
                if (playerRole != null && playerRole.technician)
                {
                    StartCoroutine(PickDoorVaultCoroutine());
                    barProgressionUI.AugmenterFillAmount();
                }
                else if (playerRole != null && !playerRole.technician)
                {
                    StartCoroutine(DisplayAndFade());
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isBeingPicked)
            {
                // Récupérer l'Animator du joueur
                Animator playerAnimator = other.GetComponent<Animator>();
                playerAnimator.SetBool("DoingAction", true);
                playerAnimator.SetBool("Walk", false);

                NavMeshAgent navMeshAgent = other.GetComponent<NavMeshAgent>();
                navMeshAgent.speed = 0;

                Unit unitActionUI = other.GetComponent<Unit>();

                if (unitActionUI != null)
                {
                    unitActionUI.actionPercingUI.SetActive(true);
                }

            }
            else
            {
                Animator playerAnimator = other.GetComponent<Animator>();
                playerAnimator.SetBool("DoingAction", false);

                NavMeshAgent navMeshAgent = other.GetComponent<NavMeshAgent>();
                navMeshAgent.speed = 3.5f;

                Unit unitActionUI = other.GetComponent<Unit>();

                if (unitActionUI != null)
                {
                    unitActionUI.actionPercingUI.SetActive(false);
                }
            }
        }
    }

    IEnumerator PickDoorVaultCoroutine()
    {
        isBeingPicked = true;
        sparkParticle.SetActive(true);
        PlayVaultStart();
        foreach (PlayerRole roleScript in _playerRole)
        {
            totalPickingTime += roleScript._pickingMultiplier * pickingDuration;
        }

        yield return new WaitForSeconds(totalPickingTime);

        UnlockDoor();
        sparkParticle.SetActive(false);
        isBeingPicked = false;
        PlayVaultEnd();
    }

    IEnumerator DisplayAndFade()
    {
        textRequiredRole.gameObject.SetActive(true);

        // Afficher le texte
        textRequiredRole.alpha = 1f;

        // Attendre pendant le temps d'affichage
        yield return new WaitForSeconds(displayTime);

        // Commencer à faire disparaître le texte progressivement
        float timer = 0f;
        while (timer < fadeTime)
        {
            // Calculer l'alpha en fonction du temps
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);

            // Appliquer l'alpha au composant TextMeshPro
            textRequiredRole.alpha = alpha;

            // Mettre à jour le timer
            timer += Time.deltaTime;

            yield return null;
        }

        textRequiredRole.gameObject.SetActive(false);
    }

    private void PlayVaultStart()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(7, audioSource);
    }

    private void PlayVaultEnd() 
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(9, audioSource);
    }

    private void PlayVaultOpen()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(10, audioSource);
    }

    private void PlayVaultClose()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(11, audioSource);
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
