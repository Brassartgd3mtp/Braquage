using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        // Fait tourner l'objet autour de l'axe Y � la vitesse sp�cifi�e
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
