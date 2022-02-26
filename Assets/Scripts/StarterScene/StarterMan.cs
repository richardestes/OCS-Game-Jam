using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterMan : MonoBehaviour
{
    public Transform Well;
    
    [Space]
    public bool StandingIdle = true;
    Vector3 StandPosition;
    
    [Space]
    public bool FellInTheWell = false;
    public float Speed = 10f;
    
    [Space]
    public Animator _animator = null;
    
    public bool MyTurn = false;
    public Rigidbody _rigid;
    
    void Start() {
        transform.LookAt(Well);
    }
    
    public void GoToWell() {
        MyTurn = true;
    }
    
    public void GoStand(Vector3 pos) {
        StandingIdle = false;
        StandPosition = pos;
    }
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(_animator != null)
            _animator.SetBool("Walk", (!StandingIdle || MyTurn));
        
        if(MyTurn) {
            transform.LookAt(Well);
            _rigid.velocity = (Well.position - transform.position).normalized * Speed;
            if(Vector3.Distance(transform.position, Well.position) <= Speed) {
                Vector3 jumpPos = transform.position;
                jumpPos.y += 3f;
                transform.position = jumpPos;
                if(_animator != null)
                    _animator.SetTrigger("Jump");
                MyTurn = false;
            }
        }
        
        if(transform.position.y <= -1f) FellInTheWell = true;
        
        if(!StandingIdle && !MyTurn) {
            transform.LookAt(StandPosition);
            _rigid.velocity = (StandPosition - transform.position).normalized * Speed;
            if(Vector3.Distance(transform.position, StandPosition) <= 1f) {StandingIdle = true; transform.LookAt(Well);}
        }
    }
}
