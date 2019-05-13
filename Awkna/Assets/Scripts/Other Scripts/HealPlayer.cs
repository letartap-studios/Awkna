public class HealPlayer : PowerUp
{
    public float healPlayer = 1;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.Heal(healPlayer);
    }
}
