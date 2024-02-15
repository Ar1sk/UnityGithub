using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesHealth : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        anim.SetTrigger("HitEnemy");

        if(health <= 0)
        {
            anim.SetTrigger("Die");
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have ngeative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
