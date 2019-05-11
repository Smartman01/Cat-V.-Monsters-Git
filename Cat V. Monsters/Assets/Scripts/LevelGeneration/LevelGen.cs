using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;

    int direction;
    public float moveAmount;

    int randRoom;

    float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool islevelGen = true;

    public LayerMask room;

    public int downCounter;

    // Start is called before the first frame update
    void Start()
    {
        int randStatingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStatingPos].position;

        randRoom = Random.Range(0, rooms.Length);
        Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && islevelGen)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);

                if (direction == 3)
                    direction = 2;
                else if (direction == 4)
                    direction = 5;
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if (roomDetection.GetComponent<RoomType>().roomType != 1 && roomDetection.GetComponent<RoomType>().roomType != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        randRoom = Random.Range(1, 4);

                        if (randRoom == 2)
                            randRoom = 1;

                        Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                randRoom = Random.Range(2, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                islevelGen = false;
            }
        }
    }
}
