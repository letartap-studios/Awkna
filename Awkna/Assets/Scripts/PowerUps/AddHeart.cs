using UnityEngine;

public class AddHeart : MonoBehaviour
{
    private void Update()
    {
        Collider2D other = Physics2D.OverlapCircle(transform.position, 1f);
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Instance.AddHealth();
        }

    }
}
