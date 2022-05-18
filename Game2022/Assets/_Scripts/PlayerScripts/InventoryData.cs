﻿using System;
using UnityEngine;

namespace PlayerScripts
{
    [Serializable]
    public class InventoryData
    {
        public GameObject[] items;
        
        public EquipmentItem Weapon;
        public EquipmentItem Armor;

        public InventoryData(InventoryData id)
        {
            items = new GameObject[8];
            Array.Copy(id.items, items, 8);
            Weapon = new EquipmentItem(id.Weapon);
            Armor = new EquipmentItem(id.Armor);
        }

        public InventoryData()
        {
            items = new GameObject[8];
        }
    }
    
    public class EquipmentItem
    {
        public GameObject Item;
        public int SlotIndex;

        public EquipmentItem(GameObject item, int slotIndex)
        {
            Item = item;
            SlotIndex = slotIndex;
        }

        public EquipmentItem(EquipmentItem ei)
        {
            Item = ei?.Item;
            if (ei != null) SlotIndex = ei.SlotIndex;
        }
    }
}