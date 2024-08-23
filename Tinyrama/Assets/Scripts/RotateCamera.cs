using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Lock cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Accumulate rotation
        rotationX += mouseX;
        rotationY -= mouseY;

        // Clamp the rotation to avoid extreme angles
        rotationY = Mathf.Clamp(rotationY, -45f, 45f);

        // Apply rotation to the camera
        virtualCamera.transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}
