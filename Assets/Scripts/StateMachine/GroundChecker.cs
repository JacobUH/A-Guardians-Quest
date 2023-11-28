using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool active;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;
    [SerializeField] private float groundMaxDistance;
    [SerializeField] private float groundMaxAngle;
    [SerializeField] private float slideStrength;

    [SerializeField] private float groundAngle;
    [SerializeField] private float groundDistance;

    public bool IsGrounded => isGrounded;
    public Vector3 slidingForce { get; private set; } //Apply when calling CharacterController.Move in MovementHandler script
    private RaycastHit hit;

    private void Update()
    {
        if (!active)
        {
            slidingForce = Vector3.zero;
            return;
        }

        if (Physics.SphereCast(this.transform.position + Vector3.up, checkRadius, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            OnLedgeDetect();

            if (hit.distance + checkRadius - 1f < groundMaxDistance)
            {
                isGrounded = true;
            }
            else isGrounded = false;
        }
        ApplySliding();
    }

    private void OnLedgeDetect()
    {
        Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit groundHit, Mathf.Infinity, groundLayer);
        groundAngle = Vector3.Angle(Vector3.up, groundHit.normal);
        groundDistance = groundHit.distance;
    }

    private void ApplySliding()
    {
        if (isGrounded && (groundDistance > groundMaxDistance || groundAngle >= groundMaxAngle))
        {
            slidingForce = hit.normal * slideStrength;
        }
        else
        {
            slidingForce = Vector3.zero;
        }
    }
}
