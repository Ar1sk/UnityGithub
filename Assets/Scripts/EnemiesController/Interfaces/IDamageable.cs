using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float DamageAmount);

    void Die();

    float MaxHealth { get; set; }

    float currentHealth { get; set; }
}