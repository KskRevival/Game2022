using System.Linq;
using UnityEngine;

namespace LabyrinthScripts
{
    public class DungeonData : MonoBehaviour
    {
        private const int wallCount = 4;
        public Vector2 offset;

        public class Cell
        {
            //можно заоптимизировать потом через 0b
            public bool visited;
            public readonly bool[] status = Enumerable.Repeat(true, wallCount).ToArray();
        }

        public int columns;
        public int rows;
        public int startPos;

        public GameObject room;
        public GameObject player;
    }
}