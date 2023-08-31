using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float drag;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] private CharacterController controller;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;
    private Vector3 force;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void FixedUpdate()
    {
        CalculateGravity();
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    private void CalculateGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    public Vector3 GetForce()
    {
        return impact + Vector3.up * verticalVelocity;
    }
    public void ApplyImpact(Vector3 impact)
    {
        this.impact += impact;
    }

    public void Jump()
    {
        if (!controller.isGrounded) return;
        verticalVelocity += Mathf.Sqrt(playerStateMachine.jumpForce * - 2f * gravity);
    }
}
