using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth;
    public float maxHunger;
    //public float maxThirst;
    private float health;
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

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
            health -= healthDrain * Time.deltaTime;
        }

        if (!starving && health < maxHealth)
        {
            health += healthGain * Time.deltaTime;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        if (health <= 0)
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
    private void death()
    {
        //Insert game over screen and ask if respawn or if quit
    }
}
