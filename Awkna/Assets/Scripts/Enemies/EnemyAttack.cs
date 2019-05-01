using UnityEngine;
using EZCameraShake;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController player;
    public float knockDuration = 0.5f;
    public float knockbackPwr = 5;
    private Vector3 playerPosition;
    private Rigidbody2D rb;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerPosition = player.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(1);

            StartCoroutine(player.Knockback(knockDuration, knockbackPwr, playerPosition, transform.position.x));

            CameraShaker.Instance.ShakeOnce(1f, 2f, .1f, .3f);                               // When the player is attacked shake the camera.

            //rb.AddForce(new Vector2(transform.position.x, transform.position.y-20));

            //Destroy(gameObject);
        }
    }

}
