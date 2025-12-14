using UnityEngine;
using System;

[Serializable]
public class SaveDataType
{
    public float xPlayerPosition;
    public float yPlayerPosition;
    public float zPlayerPosition;
    public string sceneName;

    public SaveDataType(Vector3 position, string scene)
    {
        xPlayerPosition = position.x;
        yPlayerPosition = position.y;
        zPlayerPosition = position.z;
        sceneName = scene;
    }
}
