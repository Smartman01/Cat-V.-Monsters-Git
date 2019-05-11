using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int roomType;

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
