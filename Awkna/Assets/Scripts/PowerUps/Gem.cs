using UnityEngine;

public class Gem : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().AddGem();
    }
}
