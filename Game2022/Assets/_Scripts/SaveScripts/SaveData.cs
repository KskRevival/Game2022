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
        private Player player;

        private Vector3 position;
        // public float health;
        // public float maxHealth;
        //
        // public Vector3 position;
        //
        // public GameObject[] inventory;
        
        public SaveData()
        {
            player = new Player(GameManager.Instance.player);
            position = player.transform.position;
        }
    }
}