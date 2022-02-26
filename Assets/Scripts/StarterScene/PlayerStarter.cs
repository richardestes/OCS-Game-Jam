using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour
{
    public WellWill _will;
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(!_will.isPlayerTurn() && _will.GetCurrent() != null) {
            transform.LookAt(_will.GetCurrent().gameObject.transform);
        }
    }
}
