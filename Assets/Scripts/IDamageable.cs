using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float currentHealth { get; }
    public float maxHealth { get; }

    public delegate void TakeDamageEvent(float damage);
    public event TakeDamageEvent OnTakeDamage;

    public delegate void DeathEvent(Vector3 position);
    public event DeathEvent OnDeath;

    public void TakeDamage(float damage);
}
