using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying = true;
    public static float mouseSen = 100f;
    
    public AudioMixer _mixer;
    public Slider _sfx, _music, _sen;
    
    void Start() {
        mouseSen = PlayerPrefs.GetFloat("mouseSen", 100f);
        
        _mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol", 0f));
        _mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol", 0f));
        
        if(_sfx != null) {
            _sfx.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVol", 0f));
            _music.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVol", 0f));
            _sen.SetValueWithoutNotify(mouseSen);
        }
    }
    
    public static void PlayOrPause(bool value) {
        isPlaying = value;
        if(value) Cursor.lockState = CursorLockMode.Locked; else Cursor.lockState = CursorLockMode.None;
    }
    
    public static void Play() {
        isPlaying = true;
    }
    
    public static void Pause() {
        isPlaying = false;
    }
    
    public static void LoadGame() {
        GameManager.isPlaying = true;
        SceneManager.LoadScene(3);
    }
    
    public static void LoadMenu() {
        GameManager.isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
    
    public void SetSfxVol(float volume) {
        _mixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }
    
    public void SetMusicVol(float volume) {
        _mixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }
    
    public void SetSensitivity(float value) {
        PlayerPrefs.SetFloat("mouseSen", value);
        mouseSen = value;
    }
    
    public void GoEnd() {
        GameManager.isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(5);
    }
    
    public void Exit() {
        Application.Quit();
    }
}
