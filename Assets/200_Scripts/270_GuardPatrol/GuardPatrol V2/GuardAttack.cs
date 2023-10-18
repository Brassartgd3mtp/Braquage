using UnityEngine;
using static GuardController;


public class GuardAttack : GuardBehaviour
{
    public override float DetectionRadius => detectionRadius;
    public override float DetectionAngle => detectionAngle;
    public override float NumberOfRays => numberOfRays;
    public override string PlayerTag => playerTag;

    public override void ApplyBehaviour()
    {
        // Logique d'attaque
    }

    public override BehaviourName CheckTransition()
    {

        return BehaviourName.None;
    }
}
