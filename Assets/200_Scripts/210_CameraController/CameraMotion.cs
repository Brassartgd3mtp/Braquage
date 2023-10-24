using UnityEngine;

namespace RTS_Camera
{
    // Ce script permet le d�placement de la cam�ra en r�ponse aux entr�es de d�placement horizontal et vertical.

    public class CameraMotion : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] private Vector2 _range = new Vector2(100, 100);
        [SerializeField] private Vector3 _boundsCenter = Vector3.zero; // Centre des limites


        private Vector3 _targetPosition;
        private Vector3 _input;

        private void Awake()
        {
            // Initialise la position cible au d�marrage du script.
            _targetPosition = transform.position;
        }

        private void HandleInput()
        {
            // R�cup�re les entr�es utilisateur pour le d�placement horizontal et vertical.
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            // Calcule les vecteurs de d�placement � droite et vers l'avant.
            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;

            // Combine les vecteurs de d�placement et normalise le r�sultat.
            _input = (forward + right).normalized;
        }

        private void Move()
        {
            // Calcule la prochaine position cible en fonction de l'entr�e utilisateur.
            Vector3 nextTargetPosition = _targetPosition + _input * _speed;

            // V�rifie si la prochaine position est dans les limites autoris�es.
            if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;

            // Lisse le mouvement de la cam�ra vers la position cible.
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position)
        {
            // V�rifie si la position est dans la plage autoris�e de d�placement.
            //return position.x > -_range.x &&
            //       position.x < _range.x &&
            //       position.z > -_range.y &&
            //       position.z < _range.y;
            //
            // Calculer les limites en fonction du centre
            float minX = _boundsCenter.x - _range.x;
            float maxX = _boundsCenter.x + _range.x;
            float minZ = _boundsCenter.z - _range.y;
            float maxZ = _boundsCenter.z + _range.y;

            // V�rifier si la position est dans la plage autoris�e de d�placement.
            return position.x > minX && position.x < maxX &&
                   position.z > minZ && position.z < maxZ;
        }

        private void Update()
        {
            HandleInput();
            Move();
        }

        private void OnDrawGizmos()
        {
            // Affiche des gizmos dans l'�diteur pour repr�senter la position de la cam�ra et la plage autoris�e de d�placement.
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(transform.position, 5f);
            //Gizmos.DrawWireCube(Vector3.zero, new Vector3(_range.x * 2f, 5f, _range.y * 2f));

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_boundsCenter, new Vector3(_range.x * 2f, 5f, _range.y * 2f));

        }

    }
}
