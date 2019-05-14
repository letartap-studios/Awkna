public class HealPlayer : PowerUp
{
    public float healAmount = 1;
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        PlayerStats.Instance.Heal(healAmount);
    }
}
