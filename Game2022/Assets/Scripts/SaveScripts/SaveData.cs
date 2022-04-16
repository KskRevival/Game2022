using System;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    [Serializable]
    public class SaveData
    {
        public float health, maxHealth;
        public float[] position;

        public SaveData(GameObject player)
        {
            position = new float[3];
            var position1 = player.transform.position;
            position[0] = position1.x;
            position[1] = position1.y;
            position[2] = position1.z;
            health = Player.Health;
            maxHealth = Player.MaxHealth;
        }
    }
}