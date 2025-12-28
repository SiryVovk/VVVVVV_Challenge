using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoadedSatrt : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraRoomsChange cameraRoomsChange;
    [SerializeField] private Key key;

    private void Start()
    {
        if(SaveManager.IsSaveLoaded && SaveManager.HasSave())
        {
            playerMovement.LoadData();
            cameraRoomsChange.LoadData();
            key.LoadData();
        }
        else
        {
            SaveManager.SaveData(playerMovement.transform.position, cameraRoomsChange.transform.position, SceneManager.GetActiveScene());
        }
    }
}
