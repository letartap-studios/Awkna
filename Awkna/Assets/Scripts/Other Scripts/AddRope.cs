using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRope : MonoBehaviour
{
    public float valueToAdd;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<RopeSystem>().AddRope(valueToAdd);
        }
    }
}
