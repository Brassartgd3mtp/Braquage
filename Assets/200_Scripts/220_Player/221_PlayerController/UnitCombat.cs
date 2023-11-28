using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    private UnitMovement unitMovement;
    public LayerMask guardLayer; // Variable pour stocker le layer des gardes

    public float rangeAttack = 2f;
    public float rangeRescue = 2f;


    void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si la touche E est enfoncée
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Vérifie si le joueur n'est pas immobilisé et qu'il y a un garde à proximité
            if (!unitMovement.immobilize)
            {
                EliminateGuard();
                RescueTeam();
            }
        }
    }
    private void EliminateGuard()
    {
        // Effectuer un raycast pour détecter les objets avec le layer "Guard" à proximité
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeAttack, guardLayer);

        // Parcourir tous les colliders touchés
        foreach (Collider collider in hitColliders)
        {
            Destroy(collider.gameObject);
        }
    }

    private void RescueTeam()
    {

        // Effectuer un raycast pour détecter les objets avec le tag "Player" à proximité
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, rangeRescue);

        // Parcourir tous les autres joueurs détectés
        foreach (Collider playerCollider in playerColliders)
        {
            // Vérifie si le collider a le tag "Player" et n'est pas le joueur actuel
            if (playerCollider.CompareTag("Player") && playerCollider != this.GetComponent<Collider>())
            {
                Debug.Log("Team detected!");

                // Obtenir le composant UnitMovement du joueur détecté
                UnitMovement otherUnitMovement = playerCollider.GetComponent<UnitMovement>();

                // Vérifier si le composant UnitMovement a été trouvé
                if (otherUnitMovement != null)
                {
                    otherUnitMovement.NoImmobilize();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Dessine une sphère rouge pour représenter la zone de détection dans l'éditeur Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRescue);
    }
}