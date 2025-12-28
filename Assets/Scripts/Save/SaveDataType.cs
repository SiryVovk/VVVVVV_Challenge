using UnityEngine;
using System;

[Serializable]
public class SaveDataType
{
    public float xPlayerPosition;
    public float yPlayerPosition;
    public float zPlayerPosition;

    public float xCameraPosition;
    public float yCameraPosition;
    public float zCameraPosition;

    public string sceneName;
    public bool isKeyCollected;

    public SaveDataType(Vector3 playerPosition, Vector3 cameraPosition, string scene, bool isKeyCollected)
    {
        xPlayerPosition = playerPosition.x;
        yPlayerPosition = playerPosition.y;
        zPlayerPosition = playerPosition.z;

        xCameraPosition = cameraPosition.x;
        yCameraPosition = cameraPosition.y;
        zCameraPosition = cameraPosition.z;
        
        sceneName = scene;
        this.isKeyCollected = isKeyCollected;
    }
}
