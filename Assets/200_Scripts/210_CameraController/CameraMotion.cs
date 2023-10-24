using UnityEngine;

namespace RTS_Camera
{
    // Ce script permet le déplacement de la caméra en réponse aux entrées de déplacement horizontal et vertical.

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
            // Initialise la position cible au démarrage du script.
            _targetPosition = transform.position;
        }

        private void HandleInput()
        {
            // Récupère les entrées utilisateur pour le déplacement horizontal et vertical.
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            // Calcule les vecteurs de déplacement à droite et vers l'avant.
            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;

            // Combine les vecteurs de déplacement et normalise le résultat.
            _input = (forward + right).normalized;
        }

        private void Move()
        {
            // Calcule la prochaine position cible en fonction de l'entrée utilisateur.
            Vector3 nextTargetPosition = _targetPosition + _input * _speed;

            // Vérifie si la prochaine position est dans les limites autorisées.
            if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;

            // Lisse le mouvement de la caméra vers la position cible.
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position)
        {
            // Vérifie si la position est dans la plage autorisée de déplacement.
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

            // Vérifier si la position est dans la plage autorisée de déplacement.
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
            // Affiche des gizmos dans l'éditeur pour représenter la position de la caméra et la plage autorisée de déplacement.
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(transform.position, 5f);
            //Gizmos.DrawWireCube(Vector3.zero, new Vector3(_range.x * 2f, 5f, _range.y * 2f));

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_boundsCenter, new Vector3(_range.x * 2f, 5f, _range.y * 2f));

        }

    }
}
