using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float mouseSensitivity = 10f;  // Controls how sensitive the camera is to mouse movement
    public bool invertY = false;  // Allows you to invert the Y axis, if needed

    private float xAxisValue = 0f;
    private float yAxisValue = 0f;

    void Start()
    {
        // Ensure that the freeLookCamera is assigned
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        // Lock cursor to the game window, optional but helps for focus
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust Y axis value for vertical rotation (inverted Y option)
        if (invertY)
        {
            yAxisValue += mouseY;
        }
        else
        {
            yAxisValue -= mouseY;
        }

        // Adjust X axis value for horizontal rotation
        xAxisValue += mouseX;

        // Clamp Y axis rotation so the camera doesn't flip upside down
        yAxisValue = Mathf.Clamp(yAxisValue, -40f, 80f);  // Customize this range as needed

        // Apply the input to the Cinemachine axes
        freeLookCamera.m_XAxis.Value = xAxisValue;
        freeLookCamera.m_YAxis.Value = yAxisValue;
    }
}

