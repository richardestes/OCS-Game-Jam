using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedTheExit : MonoBehaviour
{
    public GameManager _manager;
    
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") _manager.GoEnd();
    }
}
