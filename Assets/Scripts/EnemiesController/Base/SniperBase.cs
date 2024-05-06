using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SniperBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public UnityEvent OnDamage;

    public UnityEvent OnDie;

    #region StateMachine Variables
    public SniperStateMachine StateMachine { get; set; }
    public SniperIdleState IdleState { get; set; }
    public SniperChaseState ChaseState { get; set; }
    public SniperAttackState AttackState { get; set; }
    public SniperRunState RunState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackArea { get; set; }
    public bool IsInRange { get; set; }
    #endregion

    #region ScriptableObject Variables
    [SerializeField] private SniperIdleSOBase EnemyIdleBase;
    [SerializeField] private SniperChaseSOBase EnemyChaseBase;
    [SerializeField] private SniperAttackSOBase EnemyAttackBase;
    [SerializeField] private SniperRunSOBase EnemyRunBase;
    #endregion

    public SniperIdleSOBase EnemyIdleBaseInstance { get; set; }
    public SniperChaseSOBase EnemyChaseBaseInstance { get; set; }
    public SniperAttackSOBase EnemyAttackBaseInstance { get; set; }
    public SniperRunSOBase EnemyRunBaseInstance { get; set; }

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyRunBaseInstance = Instantiate(EnemyRunBase);

        StateMachine = new SniperStateMachine();

        IdleState = new SniperIdleState(this, StateMachine);
        ChaseState = new SniperChaseState(this, StateMachine);
        AttackState = new SniperAttackState(this, StateMachine);
        RunState = new SniperRunState(this, StateMachine);
    }

    private void Start()
    {
        currentHealth = MaxHealth;

        RB = GetComponent<Rigidbody2D>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyRunBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void Damage(float DamageAmount)
    {
        currentHealth -= DamageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            OnDamage.Invoke();
        }
    }

    public void Die()
    {
        OnDie.Invoke();
    }


    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckLeftorRightFacing(velocity);
    }

    public void CheckLeftorRightFacing(Vector2 velocity)
    {
        float moveDir = RB.velocity.x;

        if (moveDir > 0f && !IsFacingRight)
        {
            flip();
        }
        else if (moveDir < 0f && IsFacingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        IsFacingRight = !IsFacingRight;

        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180f;

        transform.rotation = Quaternion.Euler(rotation);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    #region Distance Area
    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetAttackingArea(bool isWithinAttackArea)
    {
        IsWithinAttackArea = isWithinAttackArea;
    }

    public void SetRunAway(bool isInRange)
    {
        IsInRange = isInRange;
    }
    #endregion

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        EnemyChase,
        EnemyIdle,
        EnemyAttack
    }
}