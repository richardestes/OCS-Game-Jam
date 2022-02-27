using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManagment : MonoBehaviour
{
    public ChannelControl _control;
    public char ChannelWork = 'R';
    
    [Space]
    public Text BlockCounter;
    public GameObject BlockPrefab;
    public Transform BlocksParent;
    
    private int count = 0;
    
    void Update() {
        if(!GameManager.isPlaying || _control.ChannelType != ChannelWork) return;
        
        BlockCounter.text = count.ToString();
        
        if(Input.GetMouseButtonUp(1)) {
            RaycastHit hit;
            if(Physics.Raycast (transform.position, transform.forward, out hit, 10f)) {
                if(hit.collider.tag == "Block") {
                    Destroy(hit.collider.gameObject);
                    count++;
                }
            }
        }
        
        if(Input.GetMouseButtonUp(0) && count > 0) {
            RaycastHit hit;
            if(Physics.Raycast (transform.position, transform.forward, out hit, 10f)) {
                count--;
                Instantiate(BlockPrefab, transform.position, BlockPrefab.transform.rotation, BlocksParent);
            }
        }
    }
}
