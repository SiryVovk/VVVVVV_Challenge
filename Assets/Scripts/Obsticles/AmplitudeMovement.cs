using System.Collections;
using UnityEngine;

public class AmplitudeMovement : MonoBehaviour
{
    [SerializeField] private float amplitudeX = 5f;
    [SerializeField] private float amplitudeY = 5f;
    [SerializeField] private float moveSpeed = 5f;

    private float maxYPosition;
    private float minYPosition;
    private float maxXPosition;
    private float minXPosition;

    private bool isMovingUp = true;

    private Coroutine moveRoutine;

    private void Awake()
    {
        maxYPosition = transform.position.y + amplitudeY;
        minYPosition = transform.position.y - amplitudeY;
        maxXPosition = transform.position.x + amplitudeX;
        minXPosition = transform.position.x - amplitudeX;
    }


    private void Update()
    {
        if(moveRoutine == null)
        {
            float xTargetPosition = isMovingUp ? maxXPosition : minXPosition;
            float yTargetPosition = isMovingUp ? maxYPosition : minYPosition;
            Vector3 targetPosition = new Vector3(xTargetPosition, yTargetPosition, transform.position.z);
            moveRoutine = StartCoroutine(MoveRoutine(targetPosition));
        }
    }

    private IEnumerator MoveRoutine(Vector3 targetPosition)
    {
        float stopDistance = 0.01f;
        while (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMovingUp = !isMovingUp;
        moveRoutine = null;
    }
}
