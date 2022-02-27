using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody;
    public Transform[] Cameras;
    
    [Space]
    public Transform toLook = null;
    public Vector3 toLookOffset = Vector3.zero;
    
    private float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!GameManager.isPlaying) return;
        
        float mouseX = Input.GetAxis("Mouse X") * GameManager.mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameManager.mouseSen * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        if(toLook != null) {
            Vector3 newLook = toLook.position + toLookOffset;
            transform.LookAt(newLook);
            for(int i=0; i < Cameras.Length; i++)
                Cameras[i].LookAt(newLook);
        }
    }
}
