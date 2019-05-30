using UnityEngine;
using EZCameraShake;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController player;
    public float knockDuration = 0.5f;
    public float knockbackPwr = 5;
    public float dealtDamage = 1;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>(); // bitch wtf 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(dealtDamage);

            player.Knockback(knockDuration, knockbackPwr, player.transform.position, transform.position.x);

            CameraShaker.Instance.ShakeOnce(1f, 2f, .1f, .3f);          // When the player is attacked shake the camera.

            StartCoroutine(player.GetInvulnerable());
        }
    }

}
