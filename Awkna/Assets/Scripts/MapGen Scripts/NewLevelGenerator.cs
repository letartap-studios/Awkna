using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelGenerator : MonoBehaviour
{
    public GameObject Room;
    public GameObject RoomLR;
    public GameObject RoomLRB;
    public GameObject RoomLRT;
    public GameObject RoomLRTB;
    public GameObject RoomTB;

    public int roomSize;
    public int mapsizeX;
    public int mapsizeY;

    //the max number of rooms to move before moving up
    public int maxStrafe;

    int cursorPosX = 0;
    int cursorPosY = 0;
    int movesTillMoveUp = 0;
    directions direction;
    directions lastDirection;
    roomsTypes[,] map;

    public enum roomsTypes
    {
        none = 0,
        room,
        roomLR,
        roomLRB,
        roomLRT,
        roomLRTB,
        roomTB,
    }

    public enum directions
    {
        none = 0,
        up,
        down,
        left,
        right,
    }

    private void changeDirection()
    {
        bool lockedLeft = false;
        bool lockedRight = false;

        lockedLeft = cursorPosX <= 0 || map[cursorPosX - 1, cursorPosY] != roomsTypes.none;
        lockedRight = cursorPosX >= mapsizeX - 1 || map[cursorPosX + 1, cursorPosY] != roomsTypes.none;

        if ((lockedLeft == false) && (lockedRight == false))
            if (1 == Random.Range(0, 2))
            {
                direction = directions.left;
                return;
            }
            else
            {
                direction = directions.right;
                return;
            }

        if ((lockedLeft == true) && (lockedRight == true))
        {

            direction = directions.up;
            movesTillMoveUp = 0;
            return;
        }

        if (lockedLeft)
        {
            direction = directions.right;
            return;
        }
        else
        {
            direction = directions.left;
            return;
        }
    }

    private void moveCursor()
    {
        switch (direction)
        {
            case directions.none:
                Debug.LogError("null direction3");
                break;
            case directions.up:
                cursorPosY--;
                break;
            case directions.down:
                cursorPosY++;
                break;
            case directions.left:
                cursorPosX--;
                break;
            case directions.right:
                cursorPosX++;
                break;
            default:
                break;
        }
    }

    private void checkMoveUp()
    {
        movesTillMoveUp--;
        if (movesTillMoveUp <= 0)
        {
            movesTillMoveUp = Random.Range(0, maxStrafe);
            direction = directions.up;
        }
    }

    private void placeRoom()
    {
        switch (direction)
        {
            case directions.none:
                Debug.LogError("null direction");
                break;
            case directions.up:
                {
                    switch (lastDirection)
                    {
                        case directions.none:
                            map[cursorPosX, cursorPosY] = roomsTypes.roomLRT;
                            break;
                        case directions.up:
                            map[cursorPosX, cursorPosY] = roomsTypes.roomTB;
                            Debug.Log("placeroom");
                            break;
                        case directions.down:
                            Debug.LogError("down");
                            break;
                        case directions.left:
                            map[cursorPosX, cursorPosY] = roomsTypes.roomLRT;
                            Debug.Log("placeroom1");
                            break;
                        case directions.right:
                            map[cursorPosX, cursorPosY] = roomsTypes.roomLRT;
                            Debug.Log("placeroom2");
                            break;
                        default:
                            Debug.LogError("null direction");
                            break;
                    }
                }
                break;
            case directions.down:
                Debug.LogError("rong direction");

                break;
            case directions.left:
            case directions.right:
                if (lastDirection == directions.up)
                {
                    map[cursorPosX, cursorPosY] = roomsTypes.roomLRB;
                }
                else
                {
                    map[cursorPosX, cursorPosY] = roomsTypes.roomLR;
                }
                break;
            default:
                Debug.LogError("null direction");
                break;
        }

    }

    void Start()
    {

        map = new roomsTypes[mapsizeX, mapsizeY];

        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                map[x, y] = roomsTypes.none;
            }
        }

        movesTillMoveUp = Random.Range(0, maxStrafe);
        cursorPosX = Random.Range(0, mapsizeX);
        Debug.Log(cursorPosX);
        cursorPosY = mapsizeY - 1;
        direction = directions.none;
        lastDirection = directions.none;
        //cursPosY starts from 0 and means bottom
        while (cursorPosY >= 0) // loop until reaches the top. Then does something else later
        {
            lastDirection = direction;
            changeDirection();
            checkMoveUp();
            placeRoom();
            moveCursor();
        }

        Vector3 v = new Vector3();
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                v = transform.position;
                v.x += x * roomSize;
                v.y += y * roomSize;

                switch (map[x, y])
                {
                    case roomsTypes.none:
                        //Instantiate(Room, v, Quaternion.identity);
                        break;
                    case roomsTypes.room:
                        Debug.LogError("errorRoom");
                        Instantiate(Room, v, Quaternion.identity);
                        break;
                    case roomsTypes.roomLR:
                        Instantiate(RoomLR, v, Quaternion.identity);
                        break;
                    case roomsTypes.roomLRB:
                        Instantiate(RoomLRB, v, Quaternion.identity);
                        break;
                    case roomsTypes.roomLRT:
                        Instantiate(RoomLRT, v, Quaternion.identity);
                        break;
                    case roomsTypes.roomLRTB:
                        Instantiate(RoomLRTB, v, Quaternion.identity);
                        break;
                    case roomsTypes.roomTB:
                        Instantiate(RoomTB, v, Quaternion.identity);
                        break;
                    default:
                        break;
                }


            }
        }

    }



}
