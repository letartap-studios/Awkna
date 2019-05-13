public class AddHeart : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.AddHealth();
    }
}
