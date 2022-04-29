using System.Collections;
using System.Collections.Generic;
using RoomGeneration;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private const int CellCount = 16;
    private const int Side = 4;

    void Start()
    {
        var item = GetItem();
        if (item == null) return;
        var pos = Random.Range(0, 16);
        var position = transform.position;
        Instantiate(
            GetItem(),
            new Vector2(
                position.x + pos / Side,
                position.y + pos % Side),
            Quaternion.identity);
    }

    GameObject GetItem()
    {
        var type = (Spawnable) GetIndex(Random.Range(0, 100), Spawnable.Item);
        return type == Spawnable.Empty
            ? null
            : GenerationData.Objects[(int) type][GetIndex(Random.Range(0, 100), type)];
    }

    int GetIndex(int gen, Spawnable spawnable)
    {
        var index = 0;
        Debug.Log(spawnable);
        while (index < (int) Spawnable.Size
               && GenerationData.Chances[(int) spawnable][index] < gen)
        {
            index++;
            gen -= GenerationData.Chances[(int) spawnable][index];
        }

        return index;
    }
}