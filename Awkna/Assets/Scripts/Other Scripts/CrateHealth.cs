﻿using UnityEngine;

public class CrateHealth : MonoBehaviour
{
    public int health;
    public GameObject[] objects;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 size;
    private LayerMask playerMask;
    public Animator anim;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
    }

    public void DestroyCrate()
    {
        for (int i = 1; i <= Random.Range(1, 5); i++) 
        {
            int rand = Random.Range(0, objects.Length);
            Instantiate(objects[rand], transform.position, Quaternion.identity);
            //AstarPath.active.Scan();
        }
        GameObject.FindWithTag("Player").GetComponent<RopeSystem>().ResetRope();
        Destroy(gameObject);
    }

    private void Update()
    {
        bool other = Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0, playerMask);

        if (!other)
        {
            return;
        }
        else
        {
            if (anim != null)
                anim.SetBool("inRange", true);
            if (Input.GetButtonDown("Interact"))
            {
                DestroyCrate();
            }
        }
    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (anim != null)
    //            anim.SetBool("inRange", true);
    //        if (Input.GetButtonDown("Interact"))
    //        {
    //            DestroyCrate();
    //        }
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null)
                anim.SetBool("inRange", false);
        }
    }
}
