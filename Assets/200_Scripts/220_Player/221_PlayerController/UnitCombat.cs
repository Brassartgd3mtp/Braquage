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
        // V�rifie si la touche E est enfonc�e
        if (Input.GetKeyDown(KeyCode.E))
        {
            // V�rifie si le joueur n'est pas immobilis� et qu'il y a un garde � proximit�
            if (!unitMovement.immobilize)
            {
                EliminateGuard();
                RescueTeam();
            }
        }
    }
    private void EliminateGuard()
    {
        // Effectuer un raycast pour d�tecter les objets avec le layer "Guard" � proximit�
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeAttack, guardLayer);

        // Parcourir tous les colliders touch�s
        foreach (Collider collider in hitColliders)
        {
            Destroy(collider.gameObject);
        }
    }

    private void RescueTeam()
    {

        // Effectuer un raycast pour d�tecter les objets avec le tag "Player" � proximit�
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, rangeRescue);

        // Parcourir tous les autres joueurs d�tect�s
        foreach (Collider playerCollider in playerColliders)
        {
            // V�rifie si le collider a le tag "Player" et n'est pas le joueur actuel
            if (playerCollider.CompareTag("Player") && playerCollider != this.GetComponent<Collider>())
            {
                Debug.Log("Team detected!");

                // Obtenir le composant UnitMovement du joueur d�tect�
                UnitMovement otherUnitMovement = playerCollider.GetComponent<UnitMovement>();

                // V�rifier si le composant UnitMovement a �t� trouv�
                if (otherUnitMovement != null)
                {
                    otherUnitMovement.NoImmobilize();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Dessine une sph�re rouge pour repr�senter la zone de d�tection dans l'�diteur Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRescue);
    }
}