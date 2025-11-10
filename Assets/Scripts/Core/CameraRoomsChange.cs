using System.Collections;
using UnityEngine;

public class CameraRoomsChange : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 roomsSize;
    [SerializeField] private float changeSpeed;

    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private Coroutine changeRoomRoutine;

    private void Awake()
    {
        targetPosition = transform.position;
        previousPosition = transform.position;
    }

    private void Update()
    {
        SetNewRoomPosition();

        if(previousPosition != targetPosition && changeRoomRoutine == null)
        {
            previousPosition = targetPosition;
            changeRoomRoutine = StartCoroutine(ChangeRoomRoutine(targetPosition));
        }
    }

    private void SetNewRoomPosition()
    {
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(playerTransform.position);

        if (viewPosition.x < 0 && changeRoomRoutine == null)
        {
            targetPosition = new Vector3(targetPosition.x - roomsSize.x, targetPosition.y, transform.position.z);

        }
        else if (viewPosition.x > 1 && changeRoomRoutine == null)
        {
            targetPosition = new Vector3(targetPosition.x + roomsSize.x, targetPosition.y, transform.position.z);
        }
        else if (viewPosition.y < 0 && changeRoomRoutine == null)
        {
            targetPosition = new Vector3(targetPosition.x, targetPosition.y - roomsSize.y, transform.position.z);
        }
        else if (viewPosition.y > 1 && changeRoomRoutine == null)
        {
            targetPosition = new Vector3(targetPosition.x, targetPosition.y + roomsSize.y, transform.position.z);
        }
    }
    
    private IEnumerator ChangeRoomRoutine(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < changeSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / changeSpeed);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
        changeRoomRoutine = null;
    }

}
