using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStarterMan : MonoBehaviour
{
    public WellWill _will;
    public Transform Well;
    
    [Space]
    public Transform MenCollection;
    public GameObject ManPrefab;
    public float TimeBetweenEachMan = 3f;
    
    [Space]
    public int maxForEachLevel= 3;
    public int MaxLevel = 10;
    
    float Level = 6.5f;
    float time = 0f;
    int count = 1;
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        time += Time.deltaTime;
        
        if(time >= TimeBetweenEachMan) {
            time = 0f;
            GameObject man = Instantiate(ManPrefab, transform.position, ManPrefab.transform.rotation, MenCollection);
            StarterMan _starter_man = man.GetComponent<StarterMan>();
            _will.men.Add(_starter_man);
            _starter_man.Well = Well;
            float theta = (2.0f * 3.14f * count) / maxForEachLevel;
            _starter_man.GoStand(new Vector3(Level * Mathf.Cos(theta), 0f, Level * Mathf.Sin(theta) ));
            count++;
            if(count > maxForEachLevel) count = 1;
        }
    }
}
