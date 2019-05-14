using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTileHealth : MonoBehaviour
{

    public int health;
    public GameObject gem;
    public GameObject gemTile;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gemTile);
            Instantiate(gem, transform.position, Quaternion.identity);
            //Destroy(gemTile);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
