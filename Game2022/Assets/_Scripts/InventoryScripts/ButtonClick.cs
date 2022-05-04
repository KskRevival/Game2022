using System.Collections;
using System.Collections.Generic;
using InventoryScripts;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    public Player player;
    
    public int SlotIndex;

    public UnityEvent leftClick;
    public UnityEvent rightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player == null) player = GameManager.Instance.player;
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                player.DragAndDropItem(SlotIndex);
                break;
            case PointerEventData.InputButton.Right:
                UseItem.UseFromSlot(SlotIndex);
                break;
        }
    }
}
