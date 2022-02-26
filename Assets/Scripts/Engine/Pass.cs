using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pass : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    
    void GoScene(UnityEngine.Video.VideoPlayer vp) {
        if(PlayerPrefs.HasKey("Done_Starter")) {
            GameManager.isPlaying = false;
            SceneManager.LoadScene(2);
        } else {
            GameManager.isPlaying = true;
            SceneManager.LoadScene(1);
        }
    }
    
    public void Start() {
        videoPlayer.loopPointReached += GoScene;
    }
}
