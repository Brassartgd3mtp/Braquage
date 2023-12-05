using UnityEngine;
using UnityEngine.UI;

namespace RTS_Camera
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private CameraMotion cameraMotion;
        public GameObject targetObject; // Référence vers le GameObject à suivre
        public int IDKeySelect;

        private void Start()
        {
            // Assure-toi que le script CameraMotion est attaché
            if (cameraMotion == null)
            {
                Debug.LogError("CameraMotion script not assigned to CameraControllerUI.");
                return;
            }

            // Assure-toi que le bouton a un Listener
            Button button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(MoveCameraToTarget);
            }
            else
            {
                Debug.LogError("Button component not found on the GameObject.");
            }
        }

        private void MoveCameraToTarget()
        {
            // Assure-toi que la référence du GameObject cible est définie
            if (targetObject != null)
            {
                // Déplace la caméra vers la position du GameObject cible
                cameraMotion.SetTarget(targetObject.transform.position);
                UnitSelections.Instance.NumKeySelect(IDKeySelect);

            }
            else
            {
                Debug.LogWarning("Target Object not set. Please assign a target GameObject in the inspector.");
            }
        }
    }
}
