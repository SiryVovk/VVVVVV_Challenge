using UnityEngine;
using System;

[Serializable]
public class SaveDataType
{
    public float xPlayerPosition;
    public float yPlayerPosition;
    public float zPlayerPosition;
    public string sceneName;
    public bool isKeyCollected;

    public SaveDataType(Vector3 position, string scene, bool isKeyCollected)
    {
        xPlayerPosition = position.x;
        yPlayerPosition = position.y;
        zPlayerPosition = position.z;
        sceneName = scene;
    }
}
