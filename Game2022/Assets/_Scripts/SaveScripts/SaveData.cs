using System;
using System.Xml.Serialization;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    //[XmlRoot("SaveData")]
    [Serializable]
    public class SaveData
    {
        public Player player;
        public float health;
        public float maxHealth;

        public Vector3 position;

        public GameObject[] inventory;
        
        public SaveData()
        {
            player = GameManager.Instance.player;
            position = player.transform.position;
            inventory = player.id.items;
        }
    }
}