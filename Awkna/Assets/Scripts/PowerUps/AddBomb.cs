using UnityEngine;
public class AddBomb : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.AddBomb();
    }
}
