using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        //можно заоптимизировать потом через 0b
        public bool visited;
        public readonly bool[] status = Enumerable.Repeat(true, 4).ToArray();
    }

    public int columns;
    public int rows;
    public int startPos;

    private Cell[] board;
    public GameObject room;
    public Vector2 offset;


    void Start()
    {
        CreateBoard();
        MazeGenerator();
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        for (var x = 0; x < columns; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                if (!board[x + y * columns].visited) continue;
                var newRoom = Instantiate(
                    room,
                    new Vector2(x * offset.x, -y * offset.y),
                    Quaternion.identity,
                    transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateRoom(board[x + y * columns].status);
                newRoom.name = "" + x + '-' + y;
            }
        }
    }

    void MazeGenerator()
    {
        var currCell = startPos;
        var path = new Stack<int>();

        while (true)
        {
            if (currCell == board.Length - 1) break;
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
                UpdateNeighbours(currCell, newCell);
                currCell = newCell;
            }
        }
    }

    void UpdateNeighbours(int currCell, int newCell)
    {
        var (curr, nc) = (Doors.Up, Doors.Down);
        var dif = newCell - currCell;
        if (dif == 1) (curr, nc) = (Doors.Right, Doors.Left);
        else if (dif == columns) (curr, nc) = (Doors.Down, Doors.Up);
        else if (dif == -1) (curr, nc) = (Doors.Left, Doors.Right);

        board[currCell].status[(int) curr] = false;
        board[newCell].status[(int) nc] = false;
    }

    void CreateBoard()
    {
        board = new Cell[columns * rows];
        for (var x = 0; x < columns; x++)
        for (var y = 0; y < rows; y++)
            board[x + columns * y] = new Cell();
    }

    List<int> CheckNeighbours(int pos)
    {
        var list = new List<int>();
        if (pos - columns >= 0 && !board[pos - columns].visited)
            list.Add(pos - columns); //Up
        if (pos + columns < board.Length && !board[pos + columns].visited)
            list.Add(pos + columns); //Down
        if ((pos + 1) % columns > 0 && !board[pos + 1].visited)
            list.Add(pos + 1); //Right
        if (pos % columns > 0 && !board[pos - 1].visited)
            list.Add(pos - 1); //Left
        return list;
    }
}