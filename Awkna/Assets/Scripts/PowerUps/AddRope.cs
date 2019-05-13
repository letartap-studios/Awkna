using UnityEngine;

public class AddRope : MonoBehaviour
{
    public float valueToAdd;
    public Vector2 areaOfEffect;
    private void Update()
    {
        Collider2D other = Physics2D.OverlapBox(transform.position, areaOfEffect, 0f);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RopeSystem>().AddRope(valueToAdd);
            Destroy(gameObject);            
        }

    }
    private void OnDrawGizmosSelected() // Draw a gismos circle around the area of effect.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, areaOfEffect);
    }
}