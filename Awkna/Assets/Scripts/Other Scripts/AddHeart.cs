using UnityEngine;

public class AddHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Instance.AddHealth();
        }

    }
}
