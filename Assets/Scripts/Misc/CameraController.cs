using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : SingletonMonobehaviour<CameraController>
{
    [SerializeField] float zoomSpeed = 3f;
    [SerializeField] float topMaxHeight = 7f;
    [SerializeField] float topMaxRadius = 4f;
    [SerializeField] float midMaxRadius = 7f;
    [SerializeField] float botMaxHeight = -5f;
    [SerializeField] float botMaxRadius = 5f;
    [Space]
    [SerializeField] float topMinHeight = 3f;
    [SerializeField] float topMinRadius = 1f;
    [SerializeField] float midMinRadius = 1.5f;
    [SerializeField] float botMinHeight = -1f;
    [SerializeField] float botMinRadius = 1f;

    CinemachineFramingTransposer midRigTransposer;

    private CinemachineFreeLook freeLook;

    protected override void Awake()
    {
        base.Awake();
        freeLook = GetComponent<CinemachineFreeLook>();
        freeLook.Follow = freeLook.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        //position camera behind player.
        freeLook.m_XAxis.Value = GameObject.FindGameObjectWithTag("Player").transform.eulerAngles.y;
        freeLook.m_YAxis.Value = 0.65f;
        midRigTransposer = freeLook.GetRig(1).GetComponent<CinemachineFramingTransposer>();
    }

    public void RotateCamera(Vector2 cameraMovement)
    {
        if (cameraMovement.x != 0 || cameraMovement.y != 0)
        {
            freeLook.m_YAxis.Value -= cameraMovement.y * Time.unscaledDeltaTime * freeLook.m_YAxis.m_MaxSpeed;
            freeLook.m_XAxis.Value += cameraMovement.x * Time.unscaledDeltaTime * freeLook.m_XAxis.m_MaxSpeed;
        }
    }

    public void ZoomIn()
    {
        if (freeLook.m_Orbits[0].m_Height > topMinHeight) freeLook.m_Orbits[0].m_Height -= zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[0].m_Height = topMinHeight;
        if (freeLook.m_Orbits[0].m_Radius > topMinRadius) freeLook.m_Orbits[0].m_Radius -= zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMinRadius;
        if (freeLook.m_Orbits[1].m_Radius > midMinRadius) freeLook.m_Orbits[1].m_Radius -= zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMinRadius;
        if (freeLook.m_Orbits[2].m_Height < botMinHeight) freeLook.m_Orbits[2].m_Height += zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[2].m_Height = botMinHeight;
        if (freeLook.m_Orbits[2].m_Radius > botMinRadius) freeLook.m_Orbits[2].m_Radius -= zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMinRadius;
    }

    public void ZoomOut()
    {
        if (freeLook.m_Orbits[0].m_Height < topMaxHeight) freeLook.m_Orbits[0].m_Height += zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[0].m_Height = topMaxHeight;
        if (freeLook.m_Orbits[0].m_Radius < topMaxRadius) freeLook.m_Orbits[0].m_Radius += zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMaxRadius;
        if (freeLook.m_Orbits[1].m_Radius < midMaxRadius) freeLook.m_Orbits[1].m_Radius += zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMaxRadius;
        if (freeLook.m_Orbits[2].m_Height > botMaxHeight) freeLook.m_Orbits[2].m_Height -= zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[2].m_Height = botMaxHeight;
        if (freeLook.m_Orbits[2].m_Radius < botMaxRadius) freeLook.m_Orbits[2].m_Radius += zoomSpeed * Time.unscaledDeltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMaxRadius;
    }
}
