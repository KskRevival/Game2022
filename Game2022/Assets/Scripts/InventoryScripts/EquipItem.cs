using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EquipItem
{
    public static Transform player = GameObject.FindWithTag("Player").transform;
    public static PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();

    static void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    public static void UseOrEquipFromSlot(int slotIndex)
    {
        Debug.Log(playerInventory);
        if (!playerInventory.HasItemInIndex(slotIndex)) return;

        var weaponComponent = playerInventory.items[slotIndex].GetComponent<WeaponScript>();
        var armorComponent = playerInventory.items[slotIndex].GetComponent<ArmorScript>();
        var healerComponent = playerInventory.items[slotIndex].GetComponent<HealerScript>();

        if (!(weaponComponent is null)) 
            player.GetComponent<PlayerEquipment>().Weapon 
                = new EquipmentItem(playerInventory.items[slotIndex], slotIndex);
    }
}
