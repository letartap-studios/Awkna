using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public float healPlayer = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Instance.Heal(healPlayer);
        }
    }
}
