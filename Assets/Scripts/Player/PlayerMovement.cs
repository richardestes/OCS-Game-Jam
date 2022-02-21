using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public float Speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    public bool isGrounded;
    
    public Vector3 v_velocity;
    
    Animator _animator;
    
    void Awake() {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(!GameManager.isPlaying) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        float z, x;
        
        bool jump = Input.GetButtonDown("Jump");
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        bool walking = x != 0 || z != 0;
        _animator.SetBool("running", walking);
        
        Vector3 move = transform.right * x + transform.forward * z;
        
        v_velocity = move * Speed;
        controller.Move(v_velocity * Time.deltaTime);
        
        if(jump && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
