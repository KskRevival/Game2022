using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        //можно заоптимизировать потом через 0b
        public bool visited;
        public bool[] status = new bool[4];
    }

    public int columns = 8;
    public int rows = 8;
    public int startPos = 0;

    private Cell[] board;

    void MazeGenerator()
    {
        var currCell = startPos;
        var path = new Stack<int>();
        var loopCount = 0;

        while (loopCount < 1000)
        {
            loopCount++;
            board[currCell].visited = true;
            var neighbours = CheckNeighbours(currCell);
            
            if (neighbours.Count == 0)
            {
                if (path.Count == 0) break;
                currCell = path.Pop();
            }
            else
            {
                path.Push(currCell);
                var newCell = neighbours[Random.Range(0, neighbours.Count)];
            }
        }
    }

    void CreateBoard()
    {
        board = new Cell[columns*rows];
        for (var x = 0; x < columns; x++)
        for (var y = 0; y < rows; y++)
            board[x * rows + y] = new Cell();
    }

    List<int> CheckNeighbours(int pos)
    {
        var list = new List<int>();
        if (pos - columns >= 0 && !board[pos - columns].visited) 
            list.Add(pos - columns);//Up
        if (pos + columns <= board.Length && !board[pos + columns].visited)
            list.Add(pos + columns);//Down
        if ((pos + 1) % columns > 0 && !board[pos + 1].visited)
            list.Add(pos + 1);      //Right
        if (pos % columns > 0 && !board[pos - 1].visited)
            list.Add(pos - 1);      //Left
    }
}