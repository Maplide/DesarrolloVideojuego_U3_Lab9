using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{

    private Camera _mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = _mainCamera.transform.forward;
    }
}
