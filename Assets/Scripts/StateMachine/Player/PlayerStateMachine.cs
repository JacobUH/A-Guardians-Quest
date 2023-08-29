using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [Header("Movement Parameters")]
    public float movementSpeed = 1.5f;
    public float changeDirectionSpeed = 15;
    public float jumpForce = 0.6f;

    [Header("Required Components")]
    public CharacterController controller;
    public Animator animator;
    public Transform mainCameraTransform;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        SwitchState(new PlayerFreeLookState(this));
    }
}
