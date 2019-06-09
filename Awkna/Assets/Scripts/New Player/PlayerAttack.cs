using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttacks;
    public float startTimeBtwAttacks;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeBtwAttacks <= 0)    // Then the player can attack.
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("attacked");
                FindObjectOfType<AudioManager>().Play("sword_hit");

                timeBtwAttacks = startTimeBtwAttacks;
            }
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

}
