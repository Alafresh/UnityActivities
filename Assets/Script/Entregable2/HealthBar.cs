using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;
    
    public void UpdateHealthbar(float maxhealth, float currentHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxhealth;
    }
}
