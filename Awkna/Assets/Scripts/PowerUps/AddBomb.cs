using UnityEngine;
public class AddBomb : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().AddBomb();
    }
}
