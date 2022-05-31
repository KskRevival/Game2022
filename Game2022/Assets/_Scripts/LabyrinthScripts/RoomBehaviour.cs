using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] corners;

    //public GameObject[] doors;

    public void UpdateRoom(bool[] closed)
    {
        for (var i = 0; i < closed.Length; i++)
        {
            //doors[i].SetActive(!closed[i]);
            walls[i].SetActive(closed[i]);
        }

        corners[(int)Corners.InnerUpLeft].SetActive(closed[(int)Doors.Up] && closed[(int)Doors.Left]);
        corners[(int)Corners.InnerUpRight].SetActive(closed[(int)Doors.Up] && closed[(int)Doors.Right]);
        corners[(int)Corners.InnerDownRight].SetActive(closed[(int)Doors.Down] && closed[(int)Doors.Right]);
        corners[(int)Corners.InnerDownLeft].SetActive(closed[(int)Doors.Down] && closed[(int)Doors.Left]);

        corners[(int)Corners.OuterUpLeft].SetActive(!(closed[(int)Doors.Up] || closed[(int)Doors.Left]));
        corners[(int)Corners.OuterUpRight].SetActive(!(closed[(int)Doors.Up] || closed[(int)Doors.Right]));
        corners[(int)Corners.OuterDownRight].SetActive(!(closed[(int)Doors.Down] || closed[(int)Doors.Right]));
        corners[(int)Corners.OuterDownLeft].SetActive(!(closed[(int)Doors.Down] || closed[(int)Doors.Left]));
    }
}