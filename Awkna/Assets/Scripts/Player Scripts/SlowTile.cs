using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to slow the player down when is on top of this tile

public class SlowTile : MonoBehaviour
{
    public PlayerController playerController;
    public float slowAmount = 3;

   private void Update()
    {
        Collider2D other = Physics2D.OverlapBox(transform.position, transform.localScale, 0);
        if (other.CompareTag("Player"))
        {
            playerController.movementSpeed -= slowAmount;
        }
    }
}
