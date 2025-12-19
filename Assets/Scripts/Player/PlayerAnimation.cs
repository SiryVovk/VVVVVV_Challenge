using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMovement;

    private static string IsMovingHorizontal = "IsMovingHorizontal";
    private static string IsMovingVertical = "IsMovingVertical";

    private void Awake()
    {
        playerMovement.OnMove += HandleMove;
    }

    private void OnDestroy()
    {
        playerMovement.OnMove -= HandleMove;
    }

    private void HandleMove(float playerXVelocity, float playerYVelocity)
    {
        if(Mathf.Abs(playerXVelocity) > 0.1f)
        {
            playerAnimator.SetBool(IsMovingHorizontal, true);
        }
        else
        {
            playerAnimator.SetBool(IsMovingHorizontal, false);
        }

        if (Mathf.Abs(playerYVelocity) > 0.1f)
        {
            playerAnimator.SetBool(IsMovingVertical, true);
        }
        else
        {
            playerAnimator.SetBool(IsMovingVertical, false);
        }
    }

}
