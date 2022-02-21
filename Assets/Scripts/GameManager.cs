using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying = true;
    
    public static void Play() {
        isPlaying = true;
    }
    
    public static void Pause() {
        isPlaying = false;
    }
}
