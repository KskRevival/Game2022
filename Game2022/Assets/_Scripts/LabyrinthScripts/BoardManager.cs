using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static Unity.Mathematics.Random;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;

    public Count wallCount = new Count(5, 9);
    public Count itemCount = new Count(1, 5);

    public GameObject exit;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] itemTiles;
    public GameObject[] enemyTiles;

    private Transform boardHolder;
    private List<Vector2> gridPositions = new List<Vector2>();

    void InitializeList()
    {
        gridPositions.Clear();

        // gridPositions = Enumerable
        //     .Range(1, columns - 1)
        //     .Select((i => Enumerable
        //         .Range(1, rows - 1)
        //         .Select((x, y) => new Vector2(x, y))))
        //     .SelectMany(v => v)
        //     .ToList();

        for (var x = 1; x < columns - 1; x++)
        for (var y = 1; y < rows - 1; y++)
            gridPositions.Add(new Vector2(x, y));
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (var x = -1; x < columns + 1; x++)
        {
            for (var y = -1; y < rows + 1; y++)
            {
                var toInstantiate = OnBorder(x, y)
                    ? wallTiles[Random.Range(0, outerWallTiles.Length)]
                    : floorTiles[Random.Range(0, floorTiles.Length)];
                var instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector2 RandomPosition()
    {
        var randomIndex = Random.Range(0, gridPositions.Count);
        var randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, Count count)
    {
        var objectCount = Random.Range(count.minimum, count.maximum + 1);

        for (var i = 0; i < objectCount; i++)
        {
            Instantiate(
                tileArray[Random.Range(0, tileArray.Length)],
                RandomPosition(), 
                Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(wallTiles, wallCount);
        LayoutObjectAtRandom(itemTiles, itemCount);
        var enemyCount = (int) Mathf.Log(level, 1.5f);
        LayoutObjectAtRandom(enemyTiles, new Count(enemyCount, enemyCount));
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    private bool OnBorder(int x, int y) => x == -1 || y == -1 || x == columns || y == rows;

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int minimum, int maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }
    }
}