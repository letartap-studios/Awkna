using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script selects a random tile from the array "objects" and instantiates it to the gameObject transform.position
public class SpawnObject : MonoBehaviour
{
    public GameObject[] tiles;
    public string file;


    Object[] objects;

    private void Awake()
    {
        if (tiles.Length == 0)
        {
            objects = Resources.LoadAll(file, typeof(GameObject));
            int rand = Random.Range(0, objects.Length);
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else
        {
            int rand = Random.Range(0, tiles.Length);//0,1,...,objects.Length-1
            GameObject instance = (GameObject)Instantiate(tiles[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
    }
}