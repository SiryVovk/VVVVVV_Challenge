using UnityEngine;

public class Key : MonoBehaviour, ILoadable
{
    private void Awake()
    {
        LoadData();
    }
    
    public void LoadData()
    {
        if(SaveManager.CurrentSave != null && SaveManager.CurrentSave.isKeyCollected)
        {
            Destroy(gameObject);
        }
    }
}
