using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject item;

    void OnTriggerEnter2D(Collider2D obj) //«Наезд» на объект
    {
        if (obj.transform.tag == "Player")
        {
            var items = obj.GetComponent<Items>();
            if (!items.IsInventoryFull())
            {
                items.AddItem(gameObject); //Если наехал игрок, то он сможет подобрать предмет
                Destroy(gameObject); //Удаление объекта с карты
            }
        }
    }
}
