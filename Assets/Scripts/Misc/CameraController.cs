using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : SingletonMonobehaviour<CameraController>
{
    [Range(0f,1f)] [SerializeField] public float cameraSensitive = 0.5f;
    [SerializeField] private float maxX = 180f;
    [SerializeField] private float minX = 50f;
    [SerializeField] private float maxY = 1f;
    [SerializeField] private float minY = 0.3f; 
    [Space]
    [SerializeField] float zoomSpeed = 3f;
    [SerializeField] float mouseZoomSpeed = 10f;
    [SerializeField] float spanSpeed = 100f;
    [Space]
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
    private Coroutine cameraSpanning;
    private float currentSensitive;

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

        freeLook.m_XAxis.m_MaxSpeed = (maxX - minX) * cameraSensitive + minX;
        freeLook.m_YAxis.m_MaxSpeed = (maxY - minY) * cameraSensitive + minY;
        currentSensitive = cameraSensitive;
    }

    private void Update()
    {
        if (currentSensitive != cameraSensitive)
        {
            freeLook.m_XAxis.m_MaxSpeed = (maxX - minX) * cameraSensitive + minX;
            freeLook.m_YAxis.m_MaxSpeed = (maxY - minY) * cameraSensitive + minY;
            currentSensitive = cameraSensitive;
        }
        if (Input.mouseScrollDelta.y > 0f) MouseZoomIn();
        else if (Input.mouseScrollDelta.y < 0f) MouseZoomOut();
    }

    private IEnumerator SpanCameraCoroutine(float targetAngle)
    {
        bool rotateClockwise;
        if ((targetAngle > freeLook.m_XAxis.Value && targetAngle - freeLook.m_XAxis.Value <= 180f) ||
            (targetAngle < freeLook.m_XAxis.Value && freeLook.m_XAxis.Value - targetAngle >= 180f))
        {
            rotateClockwise = true;
        }
        else
        {
            rotateClockwise = false;
        }

        while (freeLook.m_XAxis.Value < targetAngle - 2f || freeLook.m_XAxis.Value > targetAngle + 2f)
        {
            if (rotateClockwise) freeLook.m_XAxis.Value += Time.deltaTime * spanSpeed;
            else freeLook.m_XAxis.Value -= Time.deltaTime * spanSpeed;

            if (freeLook.m_XAxis.Value > 360f) freeLook.m_XAxis.Value -= 360f;
            else if (freeLook.m_XAxis.Value < 0f) freeLook.m_XAxis.Value += 360f;

            yield return null;
        }
        cameraSpanning = null;
    }

    public void RotateCamera()
    {
        Vector2 cameraMovement = InputReader.Instance.rightStickValue.normalized;
        if (cameraSpanning != null) return;

        //freeLook.m_XAxis.Value += cameraMovement.x * Time.deltaTime * horizontalSpeed;
        //freeLook.m_YAxis.Value -= cameraMovement.y * Time.deltaTime * verticalSpeed;
    }

    public void SpanCamera(float targetAngle)
    {
        if (cameraSpanning != null) StopCoroutine(cameraSpanning);
        cameraSpanning = StartCoroutine(SpanCameraCoroutine(targetAngle));
    }

    public void ZoomIn()
    {
        if (freeLook.m_Orbits[0].m_Height > topMinHeight) freeLook.m_Orbits[0].m_Height -= zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Height = topMinHeight;
        if (freeLook.m_Orbits[0].m_Radius > topMinRadius) freeLook.m_Orbits[0].m_Radius -= zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMinRadius;
        if (freeLook.m_Orbits[1].m_Radius > midMinRadius) freeLook.m_Orbits[1].m_Radius -= zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMinRadius;
        if (freeLook.m_Orbits[2].m_Height < botMinHeight) freeLook.m_Orbits[2].m_Height += zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Height = botMinHeight;
        if (freeLook.m_Orbits[2].m_Radius > botMinRadius) freeLook.m_Orbits[2].m_Radius -= zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMinRadius;
    }

    public void ZoomOut()
    {
        if (freeLook.m_Orbits[0].m_Height < topMaxHeight) freeLook.m_Orbits[0].m_Height += zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Height = topMaxHeight;
        if (freeLook.m_Orbits[0].m_Radius < topMaxRadius) freeLook.m_Orbits[0].m_Radius += zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMaxRadius;
        if (freeLook.m_Orbits[1].m_Radius < midMaxRadius) freeLook.m_Orbits[1].m_Radius += zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMaxRadius;
        if (freeLook.m_Orbits[2].m_Height > botMaxHeight) freeLook.m_Orbits[2].m_Height -= zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Height = botMaxHeight;
        if (freeLook.m_Orbits[2].m_Radius < botMaxRadius) freeLook.m_Orbits[2].m_Radius += zoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMaxRadius;
    }

    private void MouseZoomIn()
    {
        if (freeLook.m_Orbits[0].m_Height > topMinHeight) freeLook.m_Orbits[0].m_Height -= mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Height = topMinHeight;
        if (freeLook.m_Orbits[0].m_Radius > topMinRadius) freeLook.m_Orbits[0].m_Radius -= mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMinRadius;
        if (freeLook.m_Orbits[1].m_Radius > midMinRadius) freeLook.m_Orbits[1].m_Radius -= mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMinRadius;
        if (freeLook.m_Orbits[2].m_Height < botMinHeight) freeLook.m_Orbits[2].m_Height += mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Height = botMinHeight;
        if (freeLook.m_Orbits[2].m_Radius > botMinRadius) freeLook.m_Orbits[2].m_Radius -= mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMinRadius;
    }

    private void MouseZoomOut()
    {
        if (freeLook.m_Orbits[0].m_Height < topMaxHeight) freeLook.m_Orbits[0].m_Height += mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Height = topMaxHeight;
        if (freeLook.m_Orbits[0].m_Radius < topMaxRadius) freeLook.m_Orbits[0].m_Radius += mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[0].m_Radius = topMaxRadius;
        if (freeLook.m_Orbits[1].m_Radius < midMaxRadius) freeLook.m_Orbits[1].m_Radius += mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[1].m_Radius = midMaxRadius;
        if (freeLook.m_Orbits[2].m_Height > botMaxHeight) freeLook.m_Orbits[2].m_Height -= mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Height = botMaxHeight;
        if (freeLook.m_Orbits[2].m_Radius < botMaxRadius) freeLook.m_Orbits[2].m_Radius += mouseZoomSpeed * Time.deltaTime;
        else freeLook.m_Orbits[2].m_Radius = botMaxRadius;
    }
}
