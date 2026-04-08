using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    void Start()
    {
        PauseMenuScript.IsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Перевірка на паузу
        if (PauseMenuScript.IsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return; 
        }
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        
        player.Rotate(Vector3.up * inputX);
    }
}