using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveScripts
{
    public class SaveAndLoad : MonoBehaviour
    {
        public static void SaveGame()
        {
            var player = GameObject.Find("Player");
            var bf = new BinaryFormatter();
            using (var fs = File.Create(Application.persistentDataPath + "/MySaveData.dat"))
            {
                var data = new SaveData(player);
                bf.Serialize(fs, data);
            }

            Debug.Log("Game data saved!");
        }

        public static void LoadGame()
        {
            if (!File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
            {
                Debug.LogError("There is no save data!");
                return;
            }
            
            var bf = new BinaryFormatter();
            SaveData data;
            using (var file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open))
            {
                data = (SaveData) bf.Deserialize(file);
                file.Close();
            }

            GameObject.Find("Player").transform.position =
                new Vector3(data.position[0], data.position[1], data.position[2]);
            Debug.Log("Game data loaded!");
        }
    }
}