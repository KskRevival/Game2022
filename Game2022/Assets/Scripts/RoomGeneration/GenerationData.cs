using UnityEngine;

namespace RoomGeneration
{
    public static class GenerationData
    {
        private static readonly int[] SpawnChances =
        {
            60, //nothing
            24, //food
            8,  //weapon
            8   //monster
        };

        private static GameObject[] Food;
        private static readonly int[] FoodChances =
        {
            50, //food
            50  //water
        };

        private static GameObject[] Weapons;
        private static readonly int[] WeaponChances =
        {
            60, //meelee
            20, //pistol
            10, //rifle
            5, //shootgun
            5 //missle-launcher
        };

        private static GameObject[] Monsters;
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

        public static GameObject[][] Objects =
        {
            null,
            Food,
            Weapons,
            Monsters
        };
    }
}