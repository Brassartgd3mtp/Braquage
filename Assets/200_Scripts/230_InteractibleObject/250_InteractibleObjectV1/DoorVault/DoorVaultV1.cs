using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVaultV1 : InteractibleObject
{
    private bool isLocked = true; //Bool si la porte est verrouillée
    private bool isBeingPicked = false; //Bool si le crochetage est effectué
    private bool isOpening = false; //Bool si la porte est ouverte ou fermée
    private bool isTransitioning = false; //Bool si la porte est en train de s'ouvrir ou se fermer

    [Header("Variable")]
    public float pickingDuration = 5.0f; // Durée nécessaire pour crocheter la porte, définit la difficulté du crochetage
    public float resolutionTime = 1.0f; // Durée de la transition en secondes
    public float raycastDistance = 10f; // Distance du raycast pour détecter les joueurs

    [Header("GameObject")]
    //Récupère la barre d'UI et le GameObject du particule système
    public BarProgressionDoorVault barProgressionUI;
    public GameObject sparkParticle;

    [Header("Angle de rotation")]
    //Valeur de l'axe de rotation pour l'ouverture de la porte et la fermeture
    public float rotationToOpen = 0.0f;
    public float rotationToClose = 0.0f;

    void Start()
    {
        LockDoor();
    }

    public override void OnInteraction()
    {
        if (!isBeingPicked && isLocked)
        {
            // Si la porte est verrouillée et aucun crochetage n'est en cours, détecte le joueur le plus proche
            GetClosestPlayer();
        }
        else if (!isTransitioning)
        {
            if (!isLocked)
            {
                // Si la porte n'est pas verrouillée, ouvre ou ferme la porte en fonction de son état actuel
                if (isOpening)
                {
                    CloseDoor();
                }
                else
                {
                    OpenDoor();
                }
            }
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

    #region Méthode de crochetage avec condition
    void UnlockDoor()
    {
        isLocked = false;
    }

    void LockDoor()
    {
        isLocked = true;
    }

    System.Collections.IEnumerator PickDoorVaultCoroutine()
    {
        isBeingPicked = true;
        sparkParticle.SetActive(true);
        yield return new WaitForSeconds(pickingDuration);

        UnlockDoor();
        sparkParticle.SetActive(false);
        isBeingPicked = false;
    }
    #endregion

    #region Methode d'ouverture et fermeture de la porte
    void OpenDoor()
    {
        //Permet d'ouvrir la porte sur un axe de rotation avec les Quaternion
        StartCoroutine(AnimateDoor(transform.rotation, Quaternion.Euler(0, rotationToOpen, 0)));
    }

    void CloseDoor()
    {
        //Permet de fermer la porte sur un axe de rotation avec les Quaternion
        StartCoroutine(AnimateDoor(transform.rotation, Quaternion.Euler(0, rotationToClose, 0)));
    }


    System.Collections.IEnumerator AnimateDoor(Quaternion startRotation, Quaternion endRotation)
    {
        isTransitioning = true; // Marque le début de la transition

        float elapsedTime = 0.0f;

        while (elapsedTime < resolutionTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / resolutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Assure que la rotation finale est correcte

        isTransitioning = false; // Marque la fin de la transition
        isOpening = !isOpening; //Permet de savoir si la porte est en position ouverte ou fermée
    }
    #endregion
}
