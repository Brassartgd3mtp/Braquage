using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardDetection : MonoBehaviour
{
    public float detectionDistance = 10f;
    public LayerMask playerLayer;

    private bool isPlayerDetected = false;

    //public PlayerCache playerCache;

    public List<PlayerCache> players = new List<PlayerCache>();

    void Start()
    {
        // Trouvez tous les joueurs dans la sc�ne et ajoutez-les � la liste
        PlayerCache[] allPlayers = GameObject.FindObjectsOfType<PlayerCache>();
        players.AddRange(allPlayers);
    }

    void Update()
    {
        // Utilisez un raycast pour d�tecter le joueur
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, playerLayer))
        {
            // V�rifiez si l'un des joueurs est cach�
            foreach (PlayerCache player in players)
            {
                if (player != null && !player.isHidden)
                {
                    isPlayerDetected = true;
                    Debug.Log("Player detected!");
                    // Ajoutez ici le code pour r�agir � la d�tection du joueur
                    return; // Sortez de la boucle d�s que vous trouvez un joueur cach�
                }
            }
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    // Dessine une sph�re de gizmo pour visualiser la zone de d�tection dans l'�diteur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = isPlayerDetected ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * detectionDistance, 1f);
    }
}
