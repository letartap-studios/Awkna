using UnityEngine;


public class AddBomb : PowerUp
{
    public float valueToAdd;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().AddBomb();
    }
}
