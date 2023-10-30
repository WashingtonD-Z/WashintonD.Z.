using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _health;

    public int maxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public int currentHealth { get => _health; private set => _health = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, currentHealth);

        currentHealth -= damageTaken;
        Debug.Log("Taken Damage");
        if(damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damage);
        }

        if (currentHealth == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke(transform.position);
        }
    }
}
