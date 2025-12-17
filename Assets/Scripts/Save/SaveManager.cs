using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class SaveManager
{
    public static SaveDataType CurrentSave { get; private set; }

    public static bool IsSaveLoaded => CurrentSave != null;
    
    public static void LoadToMemory()
    {
        CurrentSave = LoadData();
    }

    public static void SaveData(Vector3 savePosition, Scene scene, bool isKeyCollected = false)
    {
        SaveDataType data = new SaveDataType(savePosition, scene.name, isKeyCollected);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public static SaveDataType LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataType data = JsonUtility.FromJson<SaveDataType>(json);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DelteSave()
    {
        string path = Application.persistentDataPath + "/save.json";

        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static bool HasSave()
    {
        string path = Application.persistentDataPath + "/save.json";
        return File.Exists(path);
    }
}
