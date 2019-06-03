using UnityEngine;
public class AddBombPack : PowerUp
{
    public int numberOfBombs;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();
        for(int i = 0; i < numberOfBombs; i++)
        {
            PlayerStats.Instance.AddBomb();
        }
    }
}
