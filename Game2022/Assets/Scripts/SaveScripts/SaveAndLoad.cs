using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    public static class SaveAndLoad
    {
        static readonly string path = Application.persistentDataPath + "Saves/save.json";

        public static void SaveGame()
        {
            GameManager.Instance.player   
        }

        public static SaveData LoadGame()
        {
            
        }
    }
}