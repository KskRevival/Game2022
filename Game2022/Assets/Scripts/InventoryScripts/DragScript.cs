using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragScript : MonoBehaviour
{
    public Image icon;

    // Update is called once per frame
    void Update()
    {
        var draggedItem = InventoryHandler.draggedItem;
        FollowCursor();
        if (draggedItem != null)
        {
            icon.sprite = draggedItem.GetComponent<SpriteRenderer>().sprite;
            SetIconAlpha(1f);
        }
        else
        {
            SetIconAlpha(0f);
            icon.sprite = null;
        }
    }

    private void FollowCursor()
    {
        var cursorLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        icon.transform.position = cursorLocation;
    }

    private void SetIconAlpha(float alphaValue)
    {
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alphaValue);
    }
}
