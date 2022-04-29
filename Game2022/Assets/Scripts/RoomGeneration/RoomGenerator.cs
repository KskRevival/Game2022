using System.Collections;
using System.Collections.Generic;
using RoomGeneration;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private const int CellCount = 16;
    private const int Side = 4;
    public GenerationData data;

    void Start()
    {
        var item = GetItem();
        if (item == null) return;
        Debug.Log("Ne null");
        var pos = Random.Range(0, 16);
        var position = transform.position;
        var loot = Instantiate(
            item,
            new Vector2(
                position.x + pos / Side,
                position.y + pos % Side),
            Quaternion.identity,
            parent: transform);
        Debug.Log(loot);
    }

    GameObject GetItem()
    {
        var type = (Spawnable) GetIndex(Random.Range(0, 100), Spawnable.Empty);
        if (type != Spawnable.Empty) Debug.Log(data.Objects.Length);
        return type == Spawnable.Empty
            ? null
            : data.Objects[(int) type][GetIndex(Random.Range(0, 100), type)];
    }

    int GetIndex(int gen, Spawnable spawnable)
    {
        var index = 0;
        while (index < GenerationData.Chances[(int) spawnable].Length
               && GenerationData.Chances[(int) spawnable][index] < gen)
        {
            gen -= GenerationData.Chances[(int) spawnable][index++];
        }
        //Debug.Log(index);
        return index;
    }
}