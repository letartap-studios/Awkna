using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script selects a random tile from the array "objects" and instantiates it to the gameObject transform.position
public class SpawnObject : MonoBehaviour
{
    public GameObject[] tiles;

    private void Start()
    {
        int rand = Random.Range(0, tiles.Length);//0,1,...,objects.Length-1
        GameObject instance = Instantiate(tiles[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}