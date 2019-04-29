using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script generates the other rooms that are not a way of the level, just random rooms.

public class SpawnRooms : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDetection==null && levelGen.stopGeneration == true)
        {
            //SPAWN RANDOM ROOM
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
