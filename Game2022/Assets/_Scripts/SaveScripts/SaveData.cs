using System;
using System.Xml.Serialization;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    //[XmlRoot("SaveData")]
    [Serializable]
    public class SaveData
    {
        public float health;
        public float maxHealth;

        public Vector3 position;

        public GameObject[] inventory;
        
        public SaveData(GameObject player)
        {
            position = player.transform.position;
            

            inventory = player.GetComponent<PlayerInventory>().items;
        }
    }
}