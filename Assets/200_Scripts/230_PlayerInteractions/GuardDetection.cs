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
        // Trouvez tous les joueurs dans la scène et ajoutez-les à la liste
        PlayerCache[] allPlayers = GameObject.FindObjectsOfType<PlayerCache>();
        players.AddRange(allPlayers);
    }

    void Update()
    {
        // Utilisez un raycast pour détecter le joueur
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, playerLayer))
        {
            // Vérifiez si l'un des joueurs est caché
            foreach (PlayerCache player in players)
            {
                if (player != null && !player.isHidden)
                {
                    isPlayerDetected = true;
                    Debug.Log("Player detected!");
                    // Ajoutez ici le code pour réagir à la détection du joueur
                    return; // Sortez de la boucle dès que vous trouvez un joueur caché
                }
            }
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    // Dessine une sphère de gizmo pour visualiser la zone de détection dans l'éditeur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = isPlayerDetected ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * detectionDistance, 1f);
    }
}
