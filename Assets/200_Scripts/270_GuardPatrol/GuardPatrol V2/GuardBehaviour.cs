using UnityEngine;
using static GuardController;

public abstract class GuardBehaviour : MonoBehaviour
{
    public abstract void ApplyBehaviour();

    public abstract BehaviourName CheckTransition();
    [Header("Detection Settings")]
    [SerializeField]
    public float detectionRadius = 7.5f;
    public abstract float DetectionRadius { get; }

    public float detectionAngle = 160f;
    public abstract float DetectionAngle { get; }

    public int numberOfRays = 20;
    public abstract float NumberOfRays { get; }

    public string playerTag = "Player";
    public abstract string PlayerTag { get; }

}