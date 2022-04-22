using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    public static class SaveAndLoad
    {
        public static void SaveGame()
        {
            var bf = new BinaryFormatter();
            using (var fs = File.Create(Application.persistentDataPath + "/MySaveData.dat"))
            {
                var data = new SaveData(GameObject.Find("Player"));
                bf.Serialize(fs, data);
            }

            Debug.Log("Game data saved!");
        }

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