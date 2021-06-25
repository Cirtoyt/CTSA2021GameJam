using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPlayScript : MonoBehaviour
{
    public Button PlayButton;
    public Text PlayButtonText;
    public Slider P1Slider;
    public Slider P2Slider;
    void Update()
    {
        if (P1Slider.value == P2Slider.value)
        {
            PlayButton.enabled = false;
            PlayButton.GetComponent<Image>().color = Color.gray;
            PlayButtonText.GetComponent<Text>().enabled = true;
        }
        else
        {
            PlayButton.enabled = true;
            PlayButton.GetComponent<Image>().color = Color.white;
            PlayButtonText.GetComponent<Text>().enabled = false;
        }
    }
}
