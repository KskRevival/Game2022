using System.IO;
using UnityEngine;

namespace SaveScripts
{
    public static class SaveAndLoad
    {
        static readonly string path = Application.persistentDataPath + "/save.txt";

        public static void SaveGame()
        {
            var data = new SaveData();
            File.Create(path); 
            File.WriteAllText(
                path, 
                JsonUtility.ToJson(data));
            
        }

        public static SaveData LoadGame()
        {
            if(!File.Exists(path)) Debug.LogError("There is no SaveData");
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }
    }
}