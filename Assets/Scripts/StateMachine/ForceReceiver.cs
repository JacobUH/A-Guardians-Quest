using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine psm;
    [SerializeField] private float gravity;
    [SerializeField] private float drag;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private CharacterController controller;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void FixedUpdate()
    {
        CalculateGravity();
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    private void CalculateGravity()
    {
        if (psm != null)
        {
            if (psm.isJumping || psm.isFalling)
            {
                if (verticalVelocity > gravity)
                {
                    verticalVelocity += gravity * Time.deltaTime * 3f;
                }
            }
            else
            {
                verticalVelocity = gravity * 0.5f;
            }
        }
        else
        {
            if (controller.isGrounded)
            {
                verticalVelocity = gravity * 100f * Time.deltaTime;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
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
        verticalVelocity = psm.jumpForce;
    }
}
