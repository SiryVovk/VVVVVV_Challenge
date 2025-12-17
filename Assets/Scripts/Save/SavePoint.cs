using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private GameObject activeSavePointSprite;
    [SerializeField] private GameObject deactiveSvePointSprite;

    private bool isDeactivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDeactivated)
        {
            return;
        }

        if(collision.gameObject.TryGetComponent(out PlayerCollisiion player))
        {
            SaveManager.SaveData(player.transform.position, SceneManager.GetActiveScene(), player.HasKey);
            DeactivatePoint();
        }
    }

    private void DeactivatePoint()
    {
        isDeactivated = true;
        activeSavePointSprite.SetActive(false);
        deactiveSvePointSprite.SetActive(true);
    }
}
