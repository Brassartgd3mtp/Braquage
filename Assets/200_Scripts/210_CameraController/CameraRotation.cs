using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GridBrushBase;
namespace RTS_Camera
{
    // Ce script permet de faire tourner la caméra autour de l'axe Y en réponse aux touches Q et E.

    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float smoothing = 5f; //Vitesse d'execution de la commande
        [SerializeField] private float rotationSpeed = 5f; // Vitesse de rotation en fonction du mouvement de la souris
        private float rotationDirection = -1f; // Plage entre -1 et 1
        public bool reverseRotation = false;

        private float targetAngle; // L'angle que la caméra doit atteindre.
        private float currentAngle; // L'angle actuel de la caméra.

        private void Awake()
        {
            // Initialise les angles au démarrage du script.
            targetAngle = transform.eulerAngles.y;
            currentAngle = targetAngle;
        }

        private void HandleInput()
        {
            // Détection du clic molette et mouvement de la souris
            if (Input.GetMouseButton(2)) // 2 représente le clic molette
            {
                targetAngle -= Input.GetAxis("Mouse X") * rotationSpeed * rotationDirection;
            }
        }

        private void Rotate()
        {
            // Interpolation linéaire pour une rotation fluide.
            currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * smoothing);

            // Applique la rotation autour de l'axe Y à la caméra.
            transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
        }

        private void Update()
        {
            HandleInput();
            Rotate();

            if (reverseRotation)
            {
                rotationDirection = 1;
            }
            else if (!reverseRotation)
            {
                rotationDirection = -1;
            }
        }
    }
}
