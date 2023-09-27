using UnityEngine;
namespace RTS_Camera
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _smoothing = 5f;

        private float _targetAngle;
        private float _currentAngle;

        private void Awake()
        {
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
        }

        private void HandleInput()
        {
            //if (!Input.GetMouseButton(1)) return;
            //_targetAngle += Input.GetAxisRaw("Mouse X") * _speed;
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("AAAAAA");
                _targetAngle -= _targetAngle * _speed;

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("EEEEEE");
                _targetAngle += _targetAngle * _speed;

            }
        }

        private void Rotate()
        {
            _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);
            transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);
        }

        private void Update()
        {
            HandleInput();
            Rotate();
        }
    }
}
