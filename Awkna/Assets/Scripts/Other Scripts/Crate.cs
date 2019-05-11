using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject crate;

    // Start is called before the first frame update
    void Start()
    {
       //float rand = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {

        bool isDestroyed = false;

        if (Input.GetButton("Fire1"))
        {
            Destroy(crate);
            isDestroyed = true;
        }

        if (isDestroyed)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand2 = Random.Range(0, objects.Length);
                Instantiate(objects[rand2], transform.position, Quaternion.identity);
            }
        }

        /*if (DestroyObject.destroyobject(crate))
        {
            for (int i = 0; i < 3; i++)
            {
                int rand2 = Random.Range(0, objects.Length);
                Instantiate(objects[rand2], transform.position, Quaternion.identity);
            }
        }*/
    }
}
