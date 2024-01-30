using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private ParticleSystem _deathEffect, hitEffect;
    private float _currentHealth;

    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthbar(_maxHealth, _currentHealth);
    }

    public void GetDamage()
    {
        _currentHealth -= Random.Range(0.5f, 1.5f);

        if( _currentHealth <= 0 )
        {            
            _healthBar.UpdateHealthbar(_maxHealth, _currentHealth);
            _deathEffect.Play();
            Time.timeScale = 0f;
        }
        else
        {
            hitEffect.Play();
            _healthBar.UpdateHealthbar(_maxHealth, _currentHealth);
        }
    }
}
