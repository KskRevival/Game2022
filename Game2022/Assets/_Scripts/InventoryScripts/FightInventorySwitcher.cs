using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class FightInventorySwitcher : MonoBehaviour
{
    public void SwitchFightInventory() => GetComponentInChildren<InventoryHandler>().SwitchInventory();
}
