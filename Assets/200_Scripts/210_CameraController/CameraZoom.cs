using UnityEngine;

namespace RTS_Camera
{
    // Ce script permet le zoom de la cam�ra en r�ponse � la molette de la souris.

    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _speed = 25f; // Vitesse de zoom de la cam�ra.
        [SerializeField] private float _smoothing = 5f; // Lissage du mouvement de zoom.
        [SerializeField] private Vector2 _range = new(30f, 70f); // Plage de zoom autoris�e.
        [SerializeField] private Transform _cameraHolder; // Transform du conteneur de la cam�ra.

        // La direction vers laquelle la cam�ra est orient�e dans l'espace local.
        private Vector3 _cameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

        private Vector3 _targetPosition;
        private float _input;

        private void Awake()
        {
            // Initialise la position cible au d�marrage du script.
            _targetPosition = _cameraHolder.localPosition;
        }

        private void HandleInput()
        {
            _input = Input.GetAxisRaw("Mouse ScrollWheel");
        }

        private void Zoom()
        {
            // Calcule la prochaine position cible en fonction de l'entr�e utilisateur.
            Vector3 nextTargetPosition = _targetPosition + _cameraDirection * (_input * _speed);

            // V�rifie si la prochaine position est dans les limites autoris�es.
            if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;

            // Lisse le d�placement du conteneur de la cam�ra vers la position cible.
            _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position)
        {
            // V�rifie si la position est dans la plage de zoom autoris�e.
            return position.magnitude > _range.x && position.magnitude < _range.y;
        }

        private void Update()
        {
            HandleInput();
            Zoom();
        }
    }
}
