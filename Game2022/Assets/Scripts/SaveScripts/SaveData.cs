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
        //[XmlElement("health")]
        public float health;
        //[XmlElement("maxHealth")]
        public float maxHealth;

        //[XmlArray("Position"), XmlArrayItem("coordinate")]
        public float[] position;

        //[XmlArray("Inventory"), XmlArrayItem("Item")]
        [NonSerialized]
        public GameObject[] inventory;
        
        public SaveData(GameObject player)
        {
            position = new float[3];
            var position1 = player.transform.position;
            position[0] = position1.x;
            position[1] = position1.y;
            position[2] = position1.z;
            
            health = Player.Health;
            maxHealth = Player.MaxHealth;

            inventory = player.GetComponent<PlayerInventory>().items;
        }
    }
}