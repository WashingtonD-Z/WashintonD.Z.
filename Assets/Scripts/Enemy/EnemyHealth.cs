using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _health;

    public float maxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public float currentHealth { get => _health; private set => _health = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    [SerializeField] private EnemyBehaviour enemyBehaviour;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = Mathf.Clamp(damage, 0, currentHealth);

        currentHealth -= damageTaken;
        Debug.Log("Taken Damage");
        if(damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damage);
        }

        if (currentHealth == 0 && damageTaken != 0)
        {
            enemyBehaviour.enabled = false;
            OnDeath?.Invoke(transform.position);
            animator.SetTrigger("isDying");
        }
    }
}
