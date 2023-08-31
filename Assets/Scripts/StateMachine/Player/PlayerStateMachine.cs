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
    public ForceReceiver forceReceiver;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forceReceiver = GetComponent<ForceReceiver>();
        SwitchState(new PlayerFreeLookState(this));
    }
}
