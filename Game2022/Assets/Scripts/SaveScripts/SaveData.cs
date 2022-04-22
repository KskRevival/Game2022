using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace SaveScripts
{
    [Serializable]
    public class SaveData
    {
        public int health, maxHealth;
        public float[] position;

        public SaveData(GameObject player)
        {
            position = new float[3];
            var position1 = player.transform.position;
            position[0] = position1.x;
            position[1] = position1.y;
            position[2] = position1.z;
            
            if (Player.healthBar == null || Player.healthBar.slider == null) return;
            Debug.Log(health);
            health = (int) Player.healthBar.slider.value;
            maxHealth = (int) Player.healthBar.slider.maxValue;
        }
    }
}