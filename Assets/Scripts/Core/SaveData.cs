using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    private int sceneIndex;

    public SaveData(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }
}
