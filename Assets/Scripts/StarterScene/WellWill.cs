using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellWill : MonoBehaviour
{
    public StarterMan Player;
    public int HowManyBefroeThePlayer = 5;
    public GameObject MenCollection;
    public List<StarterMan> men = new List<StarterMan>();
    
    StarterMan currentOne = null;
    int currentIndex = 0;
    int count = 0;
    bool PlayerTurn = false;
    
    public StarterMan GetCurrent() {
        return currentOne;
    }
    
    public bool isPlayerTurn() {
        return PlayerTurn;
    }
    
    void Update() {
        if(currentOne == null && !PlayerTurn) {
            currentIndex = Random.Range(0, men.Count -1);
            currentOne = men[currentIndex];
            currentOne.GoToWell();
        } else {
            if(PlayerTurn) {
                if(Player.FellInTheWell) {
                    PlayerPrefs.SetInt("Done_Starter", 0);
                    GameManager.LoadMenu();
                }
            } else {
                if(currentOne.FellInTheWell) { Destroy(currentOne.gameObject); currentOne = null; men.RemoveAt(currentIndex); count++; }
                if(count >= HowManyBefroeThePlayer) {
                    Player.GoToWell();
                    PlayerTurn = true;
                }
            }
        }
    }
}
