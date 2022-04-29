using UnityEngine;
using static UnityEngine.Resources;

namespace RoomGeneration
{
    public class GenerationData : MonoBehaviour
    {
        private static readonly int[] SpawnChances =
        {
            60, //nothing
            24, //food
            8,  //weapon
            8   //monster
        };

        public GameObject[] Food =
        {
            
        };
        private static readonly int[] FoodChances =
        {
            50, //food
            50  //water
        };

        public GameObject[] Weapons =
        {
            
        };
        private static readonly int[] WeaponChances =
        {
            60, //meelee
            20, //pistol
            10, //rifle
            5, //shootgun
            5 //missle-launcher
        };

        public GameObject[] Monsters =
        {
            
        };
        private static readonly int[] MonsterChances =
        {
            50, //same lvl
            25, //lower lvl
            25  //higher lvl
        };

        public static int[][] Chances =
        {
            SpawnChances,
            FoodChances,
            WeaponChances,
            MonsterChances
        };

        public GameObject[][] Objects;

        public GenerationData()
        {
            Objects = new[] 
            {
                null,
                Food,
                Weapons,
                Monsters
            };
        }
        
    }
}