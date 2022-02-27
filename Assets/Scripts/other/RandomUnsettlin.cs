using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUnsettlin : MonoBehaviour
{
    AudioSource _audio;
    
    public float minTime = 15f, maxTime = 30f;
    
    float time = 0f, currentTime = 15f;
    
    void Start() {
        _audio = GetComponent<AudioSource>();
    }
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(time < currentTime) {
            time += Time.deltaTime;
            return;
        }
        
        time = 0f;
        currentTime = Random.Range(minTime, maxTime);
        _audio.Play();
    }
}
