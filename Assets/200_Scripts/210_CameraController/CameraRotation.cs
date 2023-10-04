using System.Security.Cryptography;
using UnityEngine;
namespace RTS_Camera
{
    // Ce script permet de faire tourner la caméra autour de l'axe Y en réponse aux touches Q et E.

    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _smoothing = 5f; //Vitesse d'execution de la commande

        [SerializeField] private float _rotationAmount = 90f; // L'angle de rotation à chaque pression de touche

        private float _targetAngle; // L'angle que la caméra doit atteindre.
        private float _currentAngle; // L'angle actuel de la caméra.

        private void Awake()
        {
            // Initialise les angles au démarrage du script.
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
        }

        private void HandleInput()
        {

            // Détection de l'appui sur la touche A pour la rotation vers la gauche
            if (Input.GetKeyDown(KeyCode.A))
            {
                _targetAngle -= _rotationAmount;
            }

            // Détection de l'appui sur la touche E pour la rotation vers la droite
            if (Input.GetKeyDown(KeyCode.E))
            {
                _targetAngle += _rotationAmount;
            }
        }

        private void Rotate()
        {
            // Interpolation linéaire pour une rotation fluide.
            _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);

            // Applique la rotation autour de l'axe Y à la caméra.
            transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
        }

        private void Update()
        {
            HandleInput();
            Rotate();
        }
    }
}
