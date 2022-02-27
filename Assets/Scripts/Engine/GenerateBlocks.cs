using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlocks : MonoBehaviour
{
    public ChannelControl _control;
    public char ChannelWork = 'B';
    
    [Space]
    public GameObject BlockPrefab;
    public Transform BlocksParent;
    public float TimeBetweenEachBlock = 1.5f;
    
    private float time = 0f;
    
    void Update() {
        if(!GameManager.isPlaying || _control.ChannelType != ChannelWork) return;
        
        if(time < TimeBetweenEachBlock) {
            time += Time.deltaTime;
            return;
        }
        
        time = 0f;
        Instantiate(BlockPrefab, transform.position, BlockPrefab.transform.rotation, BlocksParent);
    }
}
