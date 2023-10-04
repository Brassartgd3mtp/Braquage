using System.Security.Cryptography;
using UnityEngine;
namespace RTS_Camera
{
    // Ce script permet de faire tourner la cam�ra autour de l'axe Y en r�ponse aux touches Q et E.

    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _smoothing = 5f; //Vitesse d'execution de la commande

        [SerializeField] private float _rotationAmount = 90f; // L'angle de rotation � chaque pression de touche

        private float _targetAngle; // L'angle que la cam�ra doit atteindre.
        private float _currentAngle; // L'angle actuel de la cam�ra.

        private void Awake()
        {
            // Initialise les angles au d�marrage du script.
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
        }

        private void HandleInput()
        {

            // D�tection de l'appui sur la touche A pour la rotation vers la gauche
            if (Input.GetKeyDown(KeyCode.A))
            {
                _targetAngle -= _rotationAmount;
            }

            // D�tection de l'appui sur la touche E pour la rotation vers la droite
            if (Input.GetKeyDown(KeyCode.E))
            {
                _targetAngle += _rotationAmount;
            }
        }

        private void Rotate()
        {
            // Interpolation lin�aire pour une rotation fluide.
            _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);

            // Applique la rotation autour de l'axe Y � la cam�ra.
            transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
        }

        private void Update()
        {
            HandleInput();
            Rotate();
        }
    }
}
