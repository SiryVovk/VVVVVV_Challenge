using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private PlayerCollisiion playerCollision;
    [SerializeField] private PlayerMovement playerMovement;

    private void OnEnable()
    {
        playerCollision.OnPlayerDeath += HandleRespawn;
    }

    private void OnDisable()
    {
        playerCollision.OnPlayerDeath -= HandleRespawn;
    }

    private void HandleRespawn()
    {
        if(!SaveManager.HasSave())
        {
            return;
        }

        SaveDataType savedData = SaveManager.LoadData();
        Vector3 respawnPosition = new Vector3(savedData.xPlayerPosition, savedData.yPlayerPosition, savedData.zPlayerPosition);
        playerCollision.transform.position = respawnPosition;
        playerMovement.Respawn();
    }
}
