using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joker : MonoBehaviour
{
    public ChannelControl _control;
    public float minTime = 0.5f, maxTime = 5.0f;
    
    private AudioSource _audio;
    private Animator _animator;
    private float time = 0f, currentTime = 0.5f;
    
    void Start() {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(time < currentTime) {
            time += Time.deltaTime;
            return;
        }
        
        _audio.Play();
        _animator.SetTrigger("Yell");
        _control.Shuffle();
        time = 0f;
        currentTime = Random.Range(minTime, maxTime);
    }
}
