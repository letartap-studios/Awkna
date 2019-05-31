using UnityEngine;

public class Gem : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.AddGem();
    }
}
