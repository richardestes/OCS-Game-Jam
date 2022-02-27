using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public ChannelControl _control;
    public GameManager _manager;
    public Transform Player;
    
    [Space]
    public float SpeedInRed = 10f;
    public float SpeedInBlue = 10f;
    public float TimeBetweenAttacks = 3.5f;
    
    [Space]
    public Transform StandPosition;
    
    private Rigidbody _rigid;
    private AudioSource _audio;
    private Animator _animator;
    
    private float time = 0f;
    private bool isPlayerNearBy = false;
    
    void Start() {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody>();
    }
    
    public void AttackPlayer() {
        if(isPlayerNearBy) _manager.GoEnd();
    }
    
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") isPlayerNearBy = true;
    }
    
    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") isPlayerNearBy = false;
    }
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(time <= TimeBetweenAttacks) time += Time.deltaTime;
        
        float Speed = _control.ChannelType == 'R' ? SpeedInRed : SpeedInBlue;
        _animator.SetBool("Walking", _control.ChannelType == 'B');
        _animator.SetBool("Running", _control.ChannelType == 'R');
        
        if(_control.ChannelType == 'R' || _control.ChannelType == 'B') {
            Vector3 target = Player.position;
            target.y = transform.position.y;
            
            transform.LookAt(target);
            _rigid.velocity = (target - transform.position).normalized * Speed;
            
            if(isPlayerNearBy && time >= TimeBetweenAttacks) {
                time = 0f;
                _animator.SetTrigger("Punch");
            }
        } else {
            transform.position = StandPosition.position;
            _animator.SetBool("Walking", false);
        }
    }
}
