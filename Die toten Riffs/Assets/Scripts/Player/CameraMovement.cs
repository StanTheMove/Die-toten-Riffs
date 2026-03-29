using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{

    // Variables
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float inputX1;
    float inputY1;


    public void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {

        if (PauseMenuScript.IsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Rotate the Camera around its local X axis

            cameraVerticalRotation -= inputY1;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


            // Rotate the Player Object and the Camera around its Y axis

            player.Rotate(Vector3.up * inputX1);


        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Collect Mouse Input

            inputX1 = Input.GetAxis("Mouse X") * mouseSensitivity;
            inputY1 = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the Camera around its local X axis

            cameraVerticalRotation -= inputY1;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


            // Rotate the Player Object and the Camera around its Y axis

            player.Rotate(Vector3.up * inputX1);

        }

    }
}