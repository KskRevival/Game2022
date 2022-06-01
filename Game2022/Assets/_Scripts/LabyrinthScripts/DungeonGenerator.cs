using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using RoomGeneration;
using UnityEngine;
using static LabyrinthScripts.DungeonData;
using Random = UnityEngine.Random;

namespace LabyrinthScripts
{
    public class DungeonGenerator : MonoBehaviour
    {
        private DungeonData data;
        private Transform container;

        private Cell[] board;

        public void Generate(DungeonData dungeonData)
        {
            data = dungeonData;
            CreateBoard();
            MazeGenerator();
            container = GameManager.Instance.roomContainer.transform;
            if (data.isFull && data.hasBigRooms) BreakWalls();
            GenerateDungeon();
            SpawnExitDoor();
        }

        public GameObject SpawnPlayer()
        {
            return Instantiate(
                //data.player,
                GameManager.GameObjectResources("Player"),
                new Vector3((data.offset.x - 1) / 2, (data.offset.y - 1) / 2, 0),
                Quaternion.identity,
                parent: transform);
        }

        void GenerateDungeon()
        {
            for (var x = 0; x < data.columns; x++)
            {
                for (var y = 0; y < data.rows; y++)
                {
                    if (!board[x + y * data.columns].visited) continue;
                    var newRoom = Instantiate(
                        data.room,
                        new Vector3(x * data.offset.x, -y * data.offset.y, 0),
                        Quaternion.identity,
                        container).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[x + y * data.columns].closed);
                    newRoom.name = "" + x + '-' + y;
                }
            }
        }
        
        void MazeGenerator()
        {
            var currCell = data.startPos;
            var path = new Stack<int>();

            while (CheckEnd(currCell))
            {
                board[currCell].visited = true;
                var neighbours = CheckNeighbours(currCell);

                if (neighbours.Count == 0)
                {
                    if (path.Count == 0) break;
                    currCell = path.Pop();
                    continue;
                }

                path.Push(currCell);
                var newCell = neighbours[Random.Range(0, neighbours.Count)];
                UpdateNeighbours(currCell, newCell);
                currCell = newCell;
            }

            board[currCell].visited = true;
        }

        bool CheckEnd(int currCell) =>
            data.isFull
                ? currCell <= board.Length
                : currCell != board.Length - 1;

        bool IsBorder(int x, int y) =>
            x == 0 ||
            y == 0 ||
            x == data.columns - 1 ||
            y == data.rows;

        private void BreakWalls()
        {
            for (var x = 1; x < data.columns - 1; x++)
            {
                for (var y = 1; y < data.rows - 1; y++)
                {
                    var currCell = x + y * data.columns;
                    var neighbours = AllInnerNeighbours(currCell);
                    var newDoors = GetOpenedDoors();
                    for (var i = 0; i < newDoors; i++)
                    {
                        var newCell = neighbours[Random.Range(0, neighbours.Count)];
                        UpdateNeighbours(currCell, newCell);
                    }
                }
            }
        }

        int GetOpenedDoors()
        {
            var index = 0;
            var gen = Random.Range(0, 100);
            while (index < BigRoomData.breakChances.Length
                   && BigRoomData.breakChances[index] < gen)
            {
                gen -= BigRoomData.breakChances[index++];
            }

            //Debug.Log(index);
            return index;
        }

        List<int> AllInnerNeighbours(int pos)
        {
            var list = new List<int>
            {
                pos - data.columns, //Up
                pos + data.columns, //Down
                pos + 1, //Right
                pos - 1 //Left
            };
            return list;
        }

        void UpdateNeighbours(int currCell, int newCell)
        {
            var (curr, nc) = (Doors.Up, Doors.Down);
            var dif = newCell - currCell;
            if (dif == 1) (curr, nc) = (Doors.Right, Doors.Left);
            else if (dif == data.columns) (curr, nc) = (Doors.Down, Doors.Up);
            else if (dif == -1) (curr, nc) = (Doors.Left, Doors.Right);

            board[currCell].closed[(int) curr] = false;
            board[newCell].closed[(int) nc] = false;
        }

        void CreateBoard()
        {
            board = new Cell[data.columns * data.rows];
            for (var x = 0; x < data.columns; x++)
            for (var y = 0; y < data.rows; y++)
                board[x + data.columns * y] = new Cell();
        }

        List<int> CheckNeighbours(int pos)
        {
            var list = new List<int>();
            if (pos - data.columns >= 0 && !board[pos - data.columns].visited)
                list.Add(pos - data.columns); //Up
            if (pos + data.columns < board.Length && !board[pos + data.columns].visited)
                list.Add(pos + data.columns); //Down
            if ((pos + 1) % data.columns > 0 && !board[pos + 1].visited)
                list.Add(pos + 1); //Right
            if (pos % data.columns > 0 && !board[pos - 1].visited)
                list.Add(pos - 1); //Left
            return list;
        }

        void SpawnExitDoor()
        {
            var doorLocation = !board[board.Length - 1].closed[(int) Doors.Left]
                ? new Vector3((data.columns - 1) * data.offset.x + data.offset.x - 1,
                    -(data.rows - 1) * data.offset.y + (data.offset.y - 1) / 2)
                : new Vector3((data.columns - 1) * data.offset.x + (data.offset.x - 1) / 2,
                    -(data.rows - 1) * data.offset.y);

            var rotationAngle = board[board.Length - 1].closed[(int) Doors.Left] ? 180 : -90;

            var door = Instantiate(
                GameManager.GameObjectResources("ExitDoor"),
                doorLocation,
                Quaternion.Euler(0, 0, rotationAngle),
                GameManager.Instance.transform);
        }
    }
}