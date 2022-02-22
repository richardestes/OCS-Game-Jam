using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody, _camera;
    
    [Space]
    public Transform toLook = null;
    public Vector3 toLookOffset = Vector3.zero;
    
    [Space]
    public Transform Target = null;
    public float SmoothSpeed = 1f;
    
    [Space]
    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    private Vector3 _startPos;
    
    [Space]
    public PostProcessVolume _volume = null;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _startPos = _camera.localPosition;
    }

    void Update()
    {
        if(!GameManager.isPlaying) return;
        
        Breath();
        
        float mouseX = Input.GetAxis("Mouse X") * GameManager.mouseSen * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
        
        if(toLook != null) {
            Vector3 newLook = toLook.position + toLookOffset;
            transform.LookAt(newLook);
            _camera.LookAt(newLook);
        }
        
        if(_volume != null) {
            if(Vector3.Dot(Vector3.forward, playerBody.InverseTransformPoint(transform.position)) >= 0) _volume.enabled = true; else _volume.enabled = false;
        }
    }
    
    private Vector3 BreathMotion() {
        Vector3 pos = Vector3.zero;
        pos.z += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }
    
    void Breath() {
        if (_camera.localPosition != _startPos) _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
        _camera.localPosition += BreathMotion(); 
    }
    
    void LateUpdate() {
        if(!GameManager.isPlaying || Target == null) return;
        
        transform.position = Vector3.Lerp(transform.position, Target.position, SmoothSpeed * Time.deltaTime);
    }
}
