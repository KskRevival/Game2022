using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UseItem
{
    public static Transform player = GameObject.FindWithTag("Player").transform;
    public static PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();

    public static void UseFromSlot(int slotIndex)
    {
        if (!playerInventory.HasItemInIndex(slotIndex)) return;

        var weaponComponent = playerInventory.items[slotIndex].GetComponent<WeaponScript>();
        var armorComponent = playerInventory.items[slotIndex].GetComponent<ArmorScript>();
        var healerComponent = playerInventory.items[slotIndex].GetComponent<HealerScript>();

        if (!(weaponComponent == null)) 
            player.GetComponent<PlayerEquipment>().Weapon 
                = player.GetComponent<PlayerEquipment>().Weapon == null 
                || player.GetComponent<PlayerEquipment>().Weapon.SlotIndex != slotIndex
                ? new EquipmentItem(playerInventory.items[slotIndex], slotIndex)
                : null;

        if (!(armorComponent == null))
            player.GetComponent<PlayerEquipment>().Armor
                = player.GetComponent<PlayerEquipment>().Armor == null
                || player.GetComponent<PlayerEquipment>().Armor.SlotIndex != slotIndex
                ? new EquipmentItem(playerInventory.items[slotIndex], slotIndex)
                : null;
    }
}
