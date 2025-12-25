using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private GameObject activeSavePointSprite;
    [SerializeField] private GameObject deactiveSvePointSprite;

    [SerializeField] private SoundData saveSound;

    private bool isDeactivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDeactivated)
        {
            return;
        }

        if(collision.gameObject.TryGetComponent(out PlayerCollisiion player))
        {
            Vector3 positionToSave = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
            SaveManager.SaveData(positionToSave, SceneManager.GetActiveScene(), player.HasKey);
            SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(saveSound)
                .AtPosition(transform.position);
            soundBuilder.Play();
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
