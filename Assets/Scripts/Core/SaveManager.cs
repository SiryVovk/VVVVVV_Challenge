using System.IO;
using UnityEditor.Overlays;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData(PlayerMovement playerMovement, Scene scene)
    {
        
    }

    public static SaveData LoadData()
    {
            return null;
    }

    public static bool HasSave()
    {
        string path = Application.persistentDataPath + "/save.dat";
        return File.Exists(path);
    }
}
