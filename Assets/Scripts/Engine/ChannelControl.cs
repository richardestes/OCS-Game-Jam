using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChannelControl : MonoBehaviour
{
    public GameObject _R, _G, _B;
    public Sprite R_Sprite, G_Sprite, B_Sprite;
    public Image[] _ChannelImages;
    public Slider GreenSlider;
    
    [Space]
    public GameObject DreamText;
    public Animator DreamTextAnim;
    
    [Space]
    public int currentChannel = 0;
    public char ChannelType = 'R';
    
    [Space]
    public float TimeBetweenEachChange = 1.5f;
    public float GreenChannelDuration = 2f;
    
    [Space]
    public GameObject BlocksParent;
    
    private float time = 0f, greenChannelTimer = 0f;
    private GameObject[] _cameras;
    private char[] _channels = {'R', 'G', 'B'};
    
    private bool GreenRecovery = false;
    
    void Start() {
        GameObject[] _cams = {_R, _G, _B};
        _cameras = _cams;
        GreenSlider.maxValue = GreenChannelDuration;
        GreenSlider.value = GreenChannelDuration;
    }
    
    public Camera GetActiveCamera() {
        return _cameras[currentChannel].GetComponent<Camera>();
    }
    
    public void Shuffle() {
        int[] _indexs = {0, 1, 2};
        System.Random rnd = new System.Random();
        _indexs = _indexs.OrderBy(x => rnd.Next()).ToArray();
        
        for(int i =0; i < 3; i++) {
            switch(_indexs[i]) {
                case 0:
                    _cameras[i] = _R;
                    _ChannelImages[i].sprite = R_Sprite;
                    _channels[i] = 'R';
                    break;
                case 1:
                    _cameras[i] = _G;
                    _ChannelImages[i].sprite = G_Sprite;
                    _channels[i] = 'G';
                    break;
                case 2:
                    _cameras[i] = _B;
                    _ChannelImages[i].sprite = B_Sprite;
                    _channels[i] = 'B';
                    break;
            }
            
            if(_channels[i] != ChannelType) {
                _ChannelImages[i].color = new Color(1f, 1f, 1f, 0.25f);
            } else {
                _ChannelImages[i].color = new Color(1f, 1f, 1f, 1f);
            }
        } 
    }
    
    void SelectSomethingElse(int i) {
        if(++i > 2) i = 0;
        SelectCamera(i);
    }
    
    void SelectCamera(int x) {
        if(GreenRecovery && _channels[x] == 'G') { SelectSomethingElse(x); return;}
        
        _cameras[x].SetActive(true);
        ChannelType = _channels[x];
        currentChannel = x;
        _ChannelImages[x].color = new Color(1f, 1f, 1f, 1f);
        
        if(ChannelType == 'B') {
            BlocksParent.SetActive(true);
        } else {
            BlocksParent.SetActive(false);
        }
        
        for(int i=0; i < 3; i++)
            if(i != x) { _cameras[i].SetActive(false); _ChannelImages[i].color = new Color(1f, 1f, 1f, 0.25f); }
    }
    
    void Update() {
        if(!GameManager.isPlaying) return;
        
        if(ChannelType == 'G') {
            greenChannelTimer += Time.deltaTime;
            GreenSlider.value = GreenSlider.value - Time.deltaTime;
            if(greenChannelTimer >= GreenChannelDuration) {
                DreamText.SetActive(true);
                DreamTextAnim.SetTrigger("Cast");
                SelectSomethingElse(currentChannel);
                GreenRecovery = true;
                greenChannelTimer = 0f;
            }
        } else if(GreenRecovery) {
            greenChannelTimer += Time.deltaTime;
            GreenSlider.value = greenChannelTimer;
            if(greenChannelTimer >= GreenChannelDuration) {
                GreenRecovery = false;
                greenChannelTimer = 0f;
            }
        }
        
        if(time < TimeBetweenEachChange) {
            time += Time.deltaTime;
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectCamera(0);
            time = 0f;
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectCamera(1);
            time = 0f;
        } else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectCamera(2);
            time = 0f;
        }
    }
}
