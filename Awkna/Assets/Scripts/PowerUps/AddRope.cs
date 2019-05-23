using UnityEngine;

public class AddRope : PowerUp
{
    public float valueToAdd;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        //GameObject.FindWithTag("Player").GetComponent<RopeSystem>().AddRope(valueToAdd);
    }

}