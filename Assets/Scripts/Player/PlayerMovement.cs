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
    Vector3 _GroundNormal;
    public Vector3 v_velocity;
    
    Animator _animator;
    
    void Awake() {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(!GameManager.isPlaying) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded) { _GroundNormal = Vector3.up; _animator.applyRootMotion = false; } else {
            RaycastHit hitInfo;
            if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundDistance)) {
                _GroundNormal = hitInfo.normal;
                isGrounded = true;
                _animator.applyRootMotion = true;
            }
        }
        
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        bool jump = Input.GetButtonDown("Jump");
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        v_velocity = move * Speed;
        controller.Move(v_velocity * Time.deltaTime);
        
        if(jump && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
        UpdateAnimator(move);
    }
    
    void UpdateAnimator(Vector3 move) {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, _GroundNormal);
        
        if(!isGrounded) _animator.SetFloat("Jump", velocity.y);
        _animator.SetFloat("Forward", move.z, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", Mathf.Atan2(move.x, move.z), 0.1f, Time.deltaTime);
        _animator.SetBool("OnGround", isGrounded);
        
        float runCycle = Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1);
        float jumpLeg = (runCycle < 0.5f ? 1 : -1) * move.z;
        
        if(isGrounded) _animator.SetFloat("JumpLeg", jumpLeg);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
