using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveScripts
{
    public static class SaveAndLoad
    {
        static readonly string path = Application.persistentDataPath + "/save.txt";

        public static void SaveGame()
        {
            var bf = new BinaryFormatter();
            using (var fs = File.Create(Application.persistentDataPath + "/MySaveData.dat"))
            {
                var data = new SaveData();
                bf.Serialize(fs, data);
            }

            Debug.Log("Game data saved!");
            
        }

        public static SaveData LoadGame()
        {
            if(!File.Exists(path)) Debug.LogError("There is no SaveData");
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }
    }
}