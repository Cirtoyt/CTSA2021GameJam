using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private float barSmoothingSpeed = 1;
    [SerializeField] private Slider player1HealthGuage;
    [SerializeField] private Slider player2HealthGuage;
    [SerializeField] private Slider player1HeavyAttackGuage;
    [SerializeField] private Slider player2HeavyAttackGuage;
    [SerializeField] private Slider ultLeftBar;
    [SerializeField] private Slider ultRightBar;

    public void UpdatePlayer1HealthGuage(float newHP, float maxHP)
    {
        StartCoroutine(UpdateSlider(player1HealthGuage, newHP, maxHP));
    }

    public void UpdatePlayer2HealthGuage(float newHP, float maxHP)
    {
        StartCoroutine(UpdateSlider(player2HealthGuage, newHP, maxHP));
    }

    public void UpdatePlayer1HeavyAttackGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(player1HeavyAttackGuage, newAmount, maxAmount));
    }

    public void UpdatePlayer2HeavyAttackGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(player2HeavyAttackGuage, newAmount, maxAmount));
    }

    public void UpdateUltGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(ultLeftBar, newAmount, maxAmount));
        StartCoroutine(UpdateSlider(ultRightBar, newAmount, maxAmount));
    }

    private IEnumerator UpdateSlider(Slider slider, float newAmount, float totalAmount)
    {
        float previousAmount = slider.value;
        float elapsed = 0;

        while (elapsed < barSmoothingSpeed)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(previousAmount, newAmount, elapsed / barSmoothingSpeed);
            yield return null;
        }

        slider.value = newAmount;
    }
}
