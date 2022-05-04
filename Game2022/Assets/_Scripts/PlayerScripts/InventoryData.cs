using UnityEngine;

namespace PlayerScripts
{
    public class InventoryData : MonoBehaviour
    {
        public GameObject[] items = new GameObject[8];
        
        public EquipmentItem Weapon;
        public EquipmentItem Armor;
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
    }
}