using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
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
            GenerateDungeon();
        }

        public GameObject SpawnPlayer()
        {
            var position = transform.position;
            return Instantiate(
                //data.player,
                GameManager.GameObjectResources("Player"),
                new Vector3(2.5f, 2.5f, 0),
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
                    newRoom.UpdateRoom(board[x + y * data.columns].status);
                    newRoom.name = "" + x + '-' + y;
                }
            }
        }

        void MazeGenerator()
        {
            var currCell = data.startPos;
            var path = new Stack<int>();

            while (currCell != board.Length - 1)
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
        }

        void UpdateNeighbours(int currCell, int newCell)
        {
            var (curr, nc) = (Doors.Up, Doors.Down);
            var dif = newCell - currCell;
            if (dif == 1) (curr, nc) = (Doors.Right, Doors.Left);
            else if (dif == data.columns) (curr, nc) = (Doors.Down, Doors.Up);
            else if (dif == -1) (curr, nc) = (Doors.Left, Doors.Right);

            board[currCell].status[(int) curr] = false;
            board[newCell].status[(int) nc] = false;
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
    }
}