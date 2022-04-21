using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;

    public void Start()
    {
    }

    public void UpdateRoom(bool[] closed)
    {
        for (var i = 0; i < closed.Length; i++)
        {
            doors[i].SetActive(!closed[i]);
            walls[i].SetActive(closed[i]);
        }
    }
}