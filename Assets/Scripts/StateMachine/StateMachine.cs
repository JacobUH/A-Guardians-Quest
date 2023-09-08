using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float movementSpeed = 1.5f;
    public float runSpeed = 2f;
    public float dodgeSpeed = 2f;
    public float changeDirectionSpeed = 15;
    public float jumpForce = 0.6f;

    [Header("Combat Parameters")]
    public float attackRange = 2f;

    [Header("Required Components")]
    public CharacterController controller;
    public Animator animator;
    public ForceReceiver forceReceiver;
    public ComboManager comboManager;

    private State currentState;

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick();
    }

    public virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forceReceiver = GetComponent<ForceReceiver>();
        comboManager = GetComponent<ComboManager>();
    }
}
