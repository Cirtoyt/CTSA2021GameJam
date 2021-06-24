using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPlayScript : MonoBehaviour
{
    public Button PlayButton;
    public Slider P1Slider;
    public Slider P2Slider;
    private bool canPlay = true;
    void Update()
    {
        if (P1Slider.value == P2Slider.value)
        {
            canPlay = false;
            PlayButton.enabled = false;
            Debug.Log("Cannot start. Players must be different characters");
        }
        else
        {
            canPlay = true;
            PlayButton.enabled = true;
            Debug.Log("Can Begin.");
        }
    }
}
