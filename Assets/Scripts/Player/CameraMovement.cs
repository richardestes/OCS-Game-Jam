using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody, _camera;
    
    float xRotation = 0f;
    
    public Transform toLook = null;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!GameManager.isPlaying) return;
        
        float mouseX, mouseY;
        mouseX = Input.GetAxis("Mouse X") * GameManager.mouseSen * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * GameManager.mouseSen * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        if(toLook != null) {
            transform.LookAt(toLook);
            _camera.LookAt(toLook);
        }
    }
}
