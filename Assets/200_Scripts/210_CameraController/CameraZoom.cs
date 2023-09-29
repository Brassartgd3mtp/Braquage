using UnityEngine;

namespace RTS_Camera
{
    // Ce script permet le zoom de la caméra en réponse à la molette de la souris.

    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _speed = 25f; // Vitesse de zoom de la caméra.
        [SerializeField] private float _smoothing = 5f; // Lissage du mouvement de zoom.
        [SerializeField] private Vector2 _range = new(30f, 70f); // Plage de zoom autorisée.
        [SerializeField] private Transform _cameraHolder; // Transform du conteneur de la caméra.

        // La direction vers laquelle la caméra est orientée dans l'espace local.
        private Vector3 _cameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

        private Vector3 _targetPosition;
        private float _input;

        private void Awake()
        {
            // Initialise la position cible au démarrage du script.
            _targetPosition = _cameraHolder.localPosition;
        }

        private void HandleInput()
        {
            _input = Input.GetAxisRaw("Mouse ScrollWheel");
        }

        private void Zoom()
        {
            // Calcule la prochaine position cible en fonction de l'entrée utilisateur.
            Vector3 nextTargetPosition = _targetPosition + _cameraDirection * (_input * _speed);

            // Vérifie si la prochaine position est dans les limites autorisées.
            if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;

            // Lisse le déplacement du conteneur de la caméra vers la position cible.
            _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position)
        {
            // Vérifie si la position est dans la plage de zoom autorisée.
            return position.magnitude > _range.x && position.magnitude < _range.y;
        }

        private void Update()
        {
            HandleInput();
            Zoom();
        }
    }
}
