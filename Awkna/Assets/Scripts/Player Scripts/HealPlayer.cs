using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public float healPlayer = 1;

    private void Start()
    {
        //  healPlayer /= 2; // divide by 2 the amount of heal the player receives
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Instance.Heal(healPlayer);
        }

    }
}
