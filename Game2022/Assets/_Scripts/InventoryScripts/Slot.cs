using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon; //Иконка, куда будет прикрепляться спрайт

    public Image equippedBorder;

    public GameObject gameObject;

    public void UpdateSlot(GameObject gameObjectToSlot) //Обновление слота
    {
        if (gameObjectToSlot != null) PlaceGameObjectToSlot(gameObjectToSlot);
        else RemoveGameObjectFromSlot();
    }

    private void PlaceGameObjectToSlot(GameObject gameObjectToSlot)
    {
        gameObject = gameObjectToSlot;
        SetIconAlpha(1f);
        icon.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void RemoveGameObjectFromSlot()
    {
        gameObject = null;
        SetIconAlpha(0f);
        icon.sprite = null;
    }

    private void SetIconAlpha(float alphaValue) =>
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alphaValue);

    private void SetEquippedBorderAlpha(float alphaValue) =>
        equippedBorder.color = new Color(equippedBorder.color.r, equippedBorder.color.g, equippedBorder.color.b, alphaValue);

    public void SetSlotAsEquipped()
    {
        SetEquippedBorderAlpha(1f);
    }

    public void SetSlotAsUnequipped()
    {
        SetEquippedBorderAlpha(0f);
    }
}