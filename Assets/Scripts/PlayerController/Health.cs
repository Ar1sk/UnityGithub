using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collider2D hitbox;

    private Animator anim;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamage;

    public UnityEvent OnHealthChanged;

    [SerializeField] private float health;

    [SerializeField] private float MAX_HEALTH;

    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        if(IsInvincible)
        {
            return;
        }

        this.health -= amount;
        OnHealthChanged.Invoke();

        if(health <= 0)
        {
            this.health = 0;
            OnDied.Invoke();
            Restart();
        }
        else
        {
            OnDamage.Invoke();
            anim.SetTrigger("getHit");
        }
    }

    public float RemainingHealthPercentage
    {
        get
        {
            return this.health / MAX_HEALTH;
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

        OnHealthChanged.Invoke();
    }

    private void Restart()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
