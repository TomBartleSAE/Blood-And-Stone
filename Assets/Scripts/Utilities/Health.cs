using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Health : MonoBehaviour
    {
        public float currentHealth;
        [SerializeField] private float maxHealth;
        [HideInInspector] public float healthMultiplier = 1f;
        
        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                MaxHealthChangedEvent?.Invoke();
            }
        }

        public event Action<GameObject> DeathEvent;
        public event Action<GameObject> DamageChangeEvent;

        public event Action MaxHealthChangedEvent;

        public void Awake()
        {
            currentHealth = maxHealth;
        }

        // TODO: Consider changing this to property
        public void ChangeHealth(float amount, GameObject perp)
        {
            currentHealth += amount * healthMultiplier;
            
            DamageChangeEvent?.Invoke(perp);

            if (currentHealth <= 0)
            {
                DeathEvent?.Invoke(gameObject);
            }
        }
    }
}