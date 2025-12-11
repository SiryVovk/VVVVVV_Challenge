using UnityEngine;

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

        if(collision.gameObject.GetComponent<PlayerMovement>())
        {
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
