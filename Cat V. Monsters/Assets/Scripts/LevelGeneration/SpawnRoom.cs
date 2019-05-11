using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public GameObject[] rooms;

    public LevelGen levelGen;

    public LayerMask room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

        if (roomDetection == null && levelGen.islevelGen == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
