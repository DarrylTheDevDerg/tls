using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard2D : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera here, or it will find it at runtime

    void Start()
    {
        if (mainCamera == null)
        {
            // If no camera is assigned, let's grab the main one in the scene
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get the direction from the sprite to the camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // We only want the rotation on the Y axis, so we zero out the X and Z
        directionToCamera.y = 0;

        // If the direction is non-zero (to avoid errors when the camera is directly overhead or below)
        if (directionToCamera != Vector3.zero)
        {
            // Apply the rotation so the sprite faces the camera, but only rotate along the Y axis
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera);
            transform.rotation = targetRotation;
        }
    }
}
