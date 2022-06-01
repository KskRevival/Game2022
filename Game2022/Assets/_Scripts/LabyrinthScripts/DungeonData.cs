using System.Linq;
using UnityEngine;

namespace LabyrinthScripts
{
    public class DungeonData : MonoBehaviour
    {
        public const int wallCount = 4;
        public Vector2 offset;
        
        public bool isFull;
        public bool hasBigRooms;

        public class Cell
        {
            //можно заоптимизировать потом через 0b
            public bool visited;
            public readonly bool[] closed = Enumerable.Repeat(true, wallCount).ToArray();
        }

        public int columns;
        public int rows;
        public int startPos;

        public GameObject room;
        public GameObject player;
    }
}