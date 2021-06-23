using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    public enum UltimateTypes : int
    {
        Combo,
        Brains,
        Brawn,
        NotReady,
    }

    [SerializeField] private float barSmoothingSpeed = 1;
    [SerializeField] private float ultimateComboDistance;
    [SerializeField] private float player1MaxHealth;
    [SerializeField] private float player2MaxHealth;
    [SerializeField] private float player1MaxHeavyAttack;
    [SerializeField] private float player2MaxHeavyAttack;
    [SerializeField] private float maxUltimate;
    [SerializeField] private Slider player1HealthGuage;
    [SerializeField] private Slider player2HealthGuage;
    [SerializeField] private Slider player1HeavyAttackGuage;
    [SerializeField] private Slider player2HeavyAttackGuage;
    [SerializeField] private Slider ultLeftBar;
    [SerializeField] private Slider ultRightBar;

    private Transform player1;
    private Transform player2;
    private float player1Health;
    private float player2Health;
    private float player1HeavyAttack;
    private float player2HeavyAttack;
    private float ultimate;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Brains").transform;
        player2 = GameObject.FindGameObjectWithTag("Brawn").transform;
        player1Health = player1MaxHealth;
        player2Health = player2MaxHealth;
        player1HeavyAttack = player1MaxHeavyAttack;
        player2HeavyAttack = player2MaxHeavyAttack;
        ultimate = maxUltimate;
    }

    public void UpdatePlayer1HealthGuage(float newHP, float maxHP)
    {
        StartCoroutine(UpdateSlider(player1HealthGuage, newHP, maxHP));
        player1Health = newHP;

        if (player1Health <= 0)
        {
            // Death state
            Debug.Log("Brains has died!");
        }
    }

    public void UpdatePlayer2HealthGuage(float newHP, float maxHP)
    {
        StartCoroutine(UpdateSlider(player2HealthGuage, newHP, maxHP));
        player2Health = newHP;

        if (player2Health <= 0)
        {
            // Death state
            Debug.Log("Brawn has died!");
        }
    }

    public void DealDamage(int playerNumber, float damage)
    {
        if (playerNumber == 1)
        {
            float newHP = player1Health - damage;
            UpdatePlayer1HealthGuage(newHP, player1MaxHealth);
        }
        else if (playerNumber == 2)
        {
            float newHP = player2Health - damage;
            UpdatePlayer2HealthGuage(newHP, player2MaxHealth);
        }
    }

    public void UpdatePlayer1HeavyAttackGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(player1HeavyAttackGuage, newAmount, maxAmount));
        player1HeavyAttack = newAmount;
    }

    public bool CheckPlayer1HeavyAttackReady()
    {
        if (player1HeavyAttack >= 1)
        {
            return true;
        }
        return false;
    }

    public void UpdatePlayer2HeavyAttackGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(player2HeavyAttackGuage, newAmount, maxAmount));
        player2HeavyAttack = newAmount;
    }

    public bool CheckPlayer2HeavyAttackReady()
    {
        if (player2HeavyAttack >= 1)
        {
            return true;
        }
        return false;
    }

    public void UpdateUltGuage(float newAmount, float maxAmount)
    {
        StartCoroutine(UpdateSlider(ultLeftBar, newAmount, maxAmount));
        StartCoroutine(UpdateSlider(ultRightBar, newAmount, maxAmount));

         if (ultLeftBar.value >= 1)
        {
            // Show ult icon
            Debug.Log("Ultimate is ready.");
        }
         else
        {
            // Remove ult icon
        }
    }

    public UltimateTypes CheckUltimateReady(int playerNumber)
    {
        if (ultimate >= 1)
        {
            if (Vector3.Distance(player1.position, player2.position) <= ultimateComboDistance)
            {
                return UltimateTypes.Combo;
            }
            else if (playerNumber == 1)
            {
                return UltimateTypes.Brains;
            }
            else if (playerNumber == 2)
            {
                return UltimateTypes.Brawn;
            }
        }
        return UltimateTypes.NotReady;
    }

    private IEnumerator UpdateSlider(Slider slider, float newAmount, float totalAmount)
    {
        float previousAmount = slider.value;
        float newAmountPct = newAmount / totalAmount;
        float elapsed = 0;

        while (elapsed < barSmoothingSpeed)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(previousAmount, newAmountPct, elapsed / barSmoothingSpeed);
            yield return null;
        }

        slider.value = newAmount;
    }
}
