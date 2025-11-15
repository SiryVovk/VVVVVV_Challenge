using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float speedMultiplyer = 1f;
    [SerializeField] private bool rotateClockwise = true;

    private float currentRotationSpeed;

    private void Awake()
    {
        RotationDirection();
    }

    private void OnValidate()
    {
        RotationDirection();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, currentRotationSpeed * speedMultiplyer * Time.deltaTime);
    }

    private void RotationDirection()
    {
        currentRotationSpeed = rotateClockwise ? -rotateSpeed : rotateSpeed;
    }
}
