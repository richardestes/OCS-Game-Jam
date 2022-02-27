using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLooking : MonoBehaviour
{
    public CameraMovement _cameraMovement;
    public Transform Joker, Monster;
    public Monster _monster;
    
    void Start() {
        _monster.enabled = false;
        StartCoroutine(Show());
    }
    
    public IEnumerator Show() {
        _cameraMovement.toLook = Joker;
        yield return new WaitForSeconds(2);
        _cameraMovement.toLook = Monster;
        yield return new WaitForSeconds(2);
        _cameraMovement.toLook = null;
        _monster.enabled = true;
    }
}
