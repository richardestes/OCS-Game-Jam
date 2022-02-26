using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Text _text;
    public Slider _slider;
    
    void Start() {
        OnChange();
    }
    
    public void OnChange() {
        _text.text = _slider.value.ToString();
    }
}
