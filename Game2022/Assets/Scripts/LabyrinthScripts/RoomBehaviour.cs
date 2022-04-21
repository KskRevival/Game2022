using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] doors;

    public bool[] testClosed = {true, true, false, false};

    public void Start()
    {
        UpdateRoom(testClosed);
    }

    public void UpdateRoom(bool[] closed)
    {
        for (var i = 0; i < closed.Length; i++) doors[i].SetActive(closed[i]);
    }
}