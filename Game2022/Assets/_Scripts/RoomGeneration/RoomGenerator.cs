using UnityEngine;

namespace RoomGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private const int CellCount = 16;
        private const int Side = 4;
        public GameObject LeftCorner;

        void Start()
        {
            var item = GetItem();
            if (item == null) return;
            var pos = Random.Range(0, CellCount);
            var position = LeftCorner.transform.position;
            var loot = Instantiate(
                item,
                new Vector2(
                    position.x + pos / Side,
                    position.y - pos % Side - 1),
                Quaternion.identity,
                parent: transform);
        }

        GameObject GetItem()
        {
            var type = (Spawnable) GetIndex(Random.Range(0, 100), Spawnable.Empty);
            return type == Spawnable.Empty
                ? null
                : GenerationData.Objects[(int) type][GetIndex(Random.Range(0, 100), type)];
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
}