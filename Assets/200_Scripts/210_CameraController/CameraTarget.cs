using UnityEngine;
using UnityEngine.UI;

namespace RTS_Camera
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private CameraMotion cameraMotion;
        public GameObject targetObject; // R�f�rence vers le GameObject � suivre
        public int IDKeySelect;

        private void Start()
        {
            // Assure-toi que le script CameraMotion est attach�
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
            // Assure-toi que la r�f�rence du GameObject cible est d�finie
            if (targetObject != null)
            {
                // D�place la cam�ra vers la position du GameObject cible
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
