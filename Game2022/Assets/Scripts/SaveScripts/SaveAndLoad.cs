using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    public static class SaveAndLoad
    {
        static readonly string path = Application.persistentDataPath + "/player.xml";
        public static void SaveGame()
        {
            var bf = new BinaryFormatter();
            using (var fs = File.Create(Application.persistentDataPath + "/MySaveData.dat"))
            {
                var data = new SaveData(Player.player);
                bf.Serialize(fs, data);
            }
        
            Debug.Log("Game data saved!");
        }

        // public static void SaveGame()
        // {
        //     File.Create(path);
        //     var data = new SaveData(GameObject.Find("Player"));
        //     XmlSaves.Save(data, path);
        // }

        // public static SaveData LoadGame()
        // {
        //     if (File.Exists(path)) return XmlSaves.Load(path);
        //     Debug.LogError("There is no save data!");
        //     return null;
        //
        // }

        public static SaveData LoadGame()
        {
            if (!File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
            {
                Debug.LogError("There is no save data!");
                return null;
            }
            
            var bf = new BinaryFormatter();
            SaveData data;
            using (var file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open))
            {
                data = (SaveData) bf.Deserialize(file);
                file.Close();
            }
            Debug.Log("Game data loaded!");
            return data;
        }
    }
}