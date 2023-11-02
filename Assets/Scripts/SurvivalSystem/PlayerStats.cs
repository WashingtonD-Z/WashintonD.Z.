using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Stats")]
     [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _health;

    public float maxHealth { get => _maxHealth; private set => _maxHealth = value; }
    public float maxHunger;
    //public float maxThirst;
    public float currentHealth { get => _health; set => _health = value; }
    private float hunger;
    //private float thirst;
    public float lightAmmo;

    private bool starving = false;

    [Header("Drains")]
    public float healthDrain;
    public float hungerDrain;
    //public float thirstDrain;

    [Header("Passive gain")]
    public float healthGain;

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hunger = maxHunger;
        //thirst = maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger >= 0)
        {
            hunger -= hungerDrain * Time.deltaTime;
            starving = false;
        }
        else
        {
            starving = true;
            currentHealth -= healthDrain * Time.deltaTime;
        }

        if (!starving && currentHealth < maxHealth)
        {
            currentHealth += healthGain * Time.deltaTime;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        if (currentHealth <= 0)
        {
            death();
        }
    }
    public void Consume(ItemType type, float statChange)
    {
        if (type == ItemType.Food)
        {
            hunger += statChange;
            if (hunger > maxHunger)
            {
                hunger = maxHunger;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = Mathf.Clamp(damage, 0, currentHealth);

        currentHealth -= damageTaken;
    }
    private void death()
    {
        //Insert game over screen and ask if respawn or if quit
    }
}
