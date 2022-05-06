using System;
using UnityEngine;

namespace PlayerScripts
{
    public class InventoryData : MonoBehaviour
    {
        public GameObject[] items = new GameObject[8];
        
        public EquipmentItem Weapon;
        public EquipmentItem Armor;

        public InventoryData(InventoryData id)
        {
            items = new GameObject[8];
            Array.Copy(id.items, items, 8);
            Weapon = new EquipmentItem(id.Weapon);
            Armor = new EquipmentItem(id.Armor);
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
            Item = ei.Item;
            SlotIndex = ei.SlotIndex;
        }
    }
}