using UnityEngine;

public class CrateHealth : MonoBehaviour
{

    public int health;
    public GameObject[] objects;
    public GameObject crate;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(crate);
            for (int i = 1; i <= 3; i++)
            {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], transform.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
