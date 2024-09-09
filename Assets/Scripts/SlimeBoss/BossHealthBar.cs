using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable bossDamageable;


    private void Awake()
    {
        GameObject slimeBoss = GameObject.FindGameObjectWithTag("SlimeBoss");
        if (slimeBoss == null)
        {
            Debug.Log("No SlimeBoss found in the scene. Make sure it has tag 'SlimeBoss'");
        }
        bossDamageable = slimeBoss.GetComponent<Damageable>();
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(bossDamageable.Health, bossDamageable.MaxHealth);
        healthBarText.text = "HP " + bossDamageable.Health + "/" + bossDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        bossDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        bossDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxhealth)
    {
        return currentHealth / maxhealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
