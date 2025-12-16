using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadedSatrt : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        if(SaveManager.IsSaveLoaded)
        {
            playerMovement.transform.position = new Vector3(SaveManager.CurrentSave.xPlayerPosition, SaveManager.CurrentSave.yPlayerPosition, SaveManager.CurrentSave.yPlayerPosition);
        }
        else
        {
            SaveManager.SaveData(playerMovement.transform.position, SceneManager.GetActiveScene());
        }
    }
}
