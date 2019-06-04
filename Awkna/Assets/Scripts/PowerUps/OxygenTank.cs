using UnityEngine;
public class OxygenTank : PowerUp
{
    [SerializeField]
    private float valueToAdd;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        GameObject.FindWithTag("OxygenBar").GetComponent<Oxygen>().AddOxygen(valueToAdd);
    }
}
