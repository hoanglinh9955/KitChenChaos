using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Animator animator;
    private string IS_WALKING = "IsWalking";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
