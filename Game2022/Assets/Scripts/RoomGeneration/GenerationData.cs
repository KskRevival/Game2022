﻿using LabyrinthScripts;
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

        private static GameObject[] Food =
        {
            GameManager.GameObjectResources("Loot/mayo"),
            GameManager.GameObjectResources("Loot/redbull")
            
        };
        private static readonly int[] FoodChances =
        {
            50, //food
            50  //water
        };

        private static GameObject[] Weapons =
        {
            GameManager.GameObjectResources("Loot/crowbar"),
            GameManager.GameObjectResources("Loot/pistol"),
            GameManager.GameObjectResources("Loot/rifle"),
            GameManager.GameObjectResources("Loot/shotgun"),
            GameManager.GameObjectResources("Loot/shotgun")
        };
        private static readonly int[] WeaponChances =
        {
            60, //meelee
            20, //pistol
            10, //rifle
            5, //shootgun
            5 //missle-launcher
        };

        private static GameObject[] Monsters =
        {
            GameManager.GameObjectResources("Monster")
            
        };
        private static readonly int[] MonsterChances =
        {
            100
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