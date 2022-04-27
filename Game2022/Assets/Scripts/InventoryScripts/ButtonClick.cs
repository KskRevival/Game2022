using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public GameObject Player;

    public int SlotIndex;

    public void OnClick()
    {
        Player.GetComponent<PlayerInventory>().DragAndDropItem(SlotIndex);
    }
}
