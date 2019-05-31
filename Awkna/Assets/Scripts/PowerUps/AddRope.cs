using UnityEngine;

public class AddRope : PowerUp
{
    /// <summary>
    /// How much the power up increases the rope length.
    /// </summary>
    [SerializeField]
    private float valueToAdd;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.AddRope(valueToAdd);
    }

}