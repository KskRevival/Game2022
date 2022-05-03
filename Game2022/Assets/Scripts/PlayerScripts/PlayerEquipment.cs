using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class PlayerEquipment : MonoBehaviour
{
    public EquipmentItem Weapon;
    public EquipmentItem Armor;

    public int GetDamage() => Weapon.Item.GetComponent<WeaponScript>().Damage;

    public List<int> GetEquippedSlotsIndexes()
    {
        var indexesArray = new List<int>();

        if (Weapon != null) indexesArray.Add(Weapon.SlotIndex);
        if (Armor != null) indexesArray.Add(Armor.SlotIndex);

        return indexesArray;
    }

    public void ReequipBySlot()
    {
        
    }
}
