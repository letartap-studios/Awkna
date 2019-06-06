using UnityEngine;

public class GrapplingHookUsages : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.AddGrapplingUsage();
    }

}