using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace SaveScripts
{
    [Serializable]
    public class SaveData
    {
        public int health;
        public float maxHealth;
        public float[] position;

        public SaveData(GameObject player)
        {
            position = new float[3];
            var position1 = player.transform.position;
            position[0] = position1.x;
            position[1] = position1.y;
            position[2] = position1.z;

            if (HealthBar.slider == null) return;
            health = (int) HealthBar.slider.value;
            maxHealth = (int) HealthBar.slider.maxValue;
        }
    }
}