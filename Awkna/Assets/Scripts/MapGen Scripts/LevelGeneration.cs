using UnityEngine;
using Pathfinding;

// this script selects a random starting position for the room to spawn

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions; //array of posible starting positions
    public GameObject[] rooms;            //array of rooms
    /*
     * index 0 --> LR
     * index 1 --> LRB
     * index 2 --> LRT
     * index 3 --> LRBT
     */


    private int direction; //direction in which 
    public float moveAmount = 20;//number of tiles per room (moveAmout x moveAmount)

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX; //min and max values of coordonates where the room can spawn
    public float maxX;
    public float maxY;
    [HideInInspector]
    public bool stopGeneration;

    public LayerMask room;

    private int upCounter;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            LvlGenMove();
            
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

        AstarPath.active.Scan();
    }

    private void LvlGenMove() //level generation moves in a certain direction depending on the value of the direction variable
    {
        if (direction == 1 || direction == 2) //Move RIGHT !
        {
            upCounter = 0;

            if (transform.position.x < maxX) //check if the levelGenerator has reached the limit
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); //if the levelGenerator moved right, it keeps moving right,  
                                                // so it doesn't generate a room o the same space as before
                if (direction == 3)
                {
                    direction = 2; //does not move left and continues moving right
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else // the levelGenerator has reached the limit of the level to the right
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Move LEFT!
        {
            upCounter = 0;

            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5; //the levelGenerator has reached the limit of the level to the left
            }
        }
        else if (direction == 5) //Move UP!
        {
            upCounter++;

            if (transform.position.y < maxY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 2 && roomDetection.GetComponent<RoomType>().type != 3) // if the room is not good
                {
                    if (upCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction(); // the room is destroyed 

                        int randBottomRoom = Random.Range(2, 4); // in its place there spawns a room of type 2 or 3                    
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                transform.position = newPos;

                int rand = Random.Range(1, 4);
                if (rand == 2)
                {
                    rand = 1;
                }
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else  //STOP LEVEL GENERATION!
            {
                stopGeneration = true;
            }

        }
    }
}
