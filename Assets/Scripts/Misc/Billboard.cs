using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Awake()
    {
        SetCamera();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }

    void SetCamera()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }
}