using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject Player;

    public int SlotIndex;

    public UnityEvent leftClick;
    public UnityEvent rightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Player.GetComponent<PlayerInventory>().DragAndDropItem(SlotIndex);

        if (eventData.button == PointerEventData.InputButton.Right)
            EquipItem.UseOrEquipFromSlot(SlotIndex);
    }
}
