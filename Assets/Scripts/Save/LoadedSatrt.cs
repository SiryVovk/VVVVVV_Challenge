using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadedSatrt : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        if(SaveManager.IsSaveLoaded && SaveManager.HasSave())
        {
            playerMovement.LoadData();
        }
        else
        {
            SaveManager.SaveData(playerMovement.transform.position, SceneManager.GetActiveScene());
        }
    }
}
