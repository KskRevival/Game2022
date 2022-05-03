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

    private bool IsWeapon(GameObject item) => item.GetComponent<WeaponScript>() != null;

    public List<int> GetEquippedSlotsIndexes()
    {
        var indexesArray = new List<int>();

        if (Weapon != null) indexesArray.Add(Weapon.SlotIndex);
        if (Armor != null) indexesArray.Add(Armor.SlotIndex);

        return indexesArray;
    }

    public void ReequipItem(GameObject item, int slotIndex)
    {
        if (IsWeapon(item)) Weapon.SlotIndex = slotIndex;
        else Armor.SlotIndex = slotIndex;
    }
}
