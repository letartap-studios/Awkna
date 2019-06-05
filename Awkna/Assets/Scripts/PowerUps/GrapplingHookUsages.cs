using UnityEngine;

public class GrapplingHookUsages : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        RopeSystem.Instance.AddUsage();
    }

}