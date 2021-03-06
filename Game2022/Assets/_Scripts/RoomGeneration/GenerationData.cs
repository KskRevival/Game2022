using UnityEngine;

namespace RoomGeneration
{
    public static class GenerationData
    {
        private static readonly int[] SpawnChances =
        {
            60, //nothing
            20, //ammo
            7, //food
            6, //monster
            5, //weapon
            3 //armor
        };

        private static readonly int[] AmmoChances =
        {
            100
        };

        private static GameObject[] Ammo =
        {
            GameManager.GameObjectResources("Loot/ammo")
        };

        private static GameObject[] Food =
        {
            GameManager.GameObjectResources("Loot/redbull"),
            GameManager.GameObjectResources("Loot/mayo"),
            GameManager.GameObjectResources("Loot/egor")
        };

        private static readonly int[] FoodChances =
        {
            50, //water
            25, //mayo
            25  //egor
        };

        private static GameObject[] Monsters =
        {
            GameManager.GameObjectResources("Monster"),
            GameManager.GameObjectResources("Entity10")
        };

        private static readonly int[] MonsterChances =
        {
            60,
            40
        };

        private static GameObject[] Weapons =
        {
            GameManager.GameObjectResources("Loot/crowbar"),
            GameManager.GameObjectResources("Loot/pistol"),
            GameManager.GameObjectResources("Loot/rifle"),
            GameManager.GameObjectResources("Loot/shotgun"),
            GameManager.GameObjectResources("Loot/grenade")
        };

        private static readonly int[] WeaponChances =
        {
            60, //meelee
            20, //pistol
            10, //rifle
            5, //shootgun
            5 //grenade
        };

        private static readonly GameObject[] Armor =
        {
            GameManager.GameObjectResources("Loot/ArmorBad"),
            GameManager.GameObjectResources("Loot/ArmorGood")
        };

        private static readonly int[] ArmorChances =
        {
            75,
            25
        };

        public static int[][] Chances =
        {
            SpawnChances,
            AmmoChances,
            FoodChances,
            MonsterChances,
            WeaponChances,
            ArmorChances
        };

        public static GameObject[][] Objects =
        {
            null,
            Ammo,
            Food,
            Monsters,
            Weapons,
            Armor
        };
    }
}