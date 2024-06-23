using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public UnityEvent OnDamage;

    public UnityEvent OnDie;

    #region StateMachine Variables
    public SoldierStateMachine StateMachine { get; set; }
    public SoldierIdleState IdleState { get; set; }
    public SoldierChaseState ChaseState { get; set; }
    public SoldierAttackState AttackState { get; set; }
    public SoldierRunState RunState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackArea { get; set; }
    public bool IsInRange { get; set; }
    #endregion

    #region ScriptableObject Variables
    [SerializeField] private SoldierIdleSOBase EnemyIdleBase;
    [SerializeField] private SoldierChaseSOBase EnemyChaseBase;
    [SerializeField] private SoldierAttackSOBase EnemyAttackBase;
    [SerializeField] private SoldierRunSOBase EnemyRunBase;
    #endregion

    public SoldierIdleSOBase EnemyIdleBaseInstance { get; set; }
    public SoldierChaseSOBase EnemyChaseBaseInstance { get; set; }
    public SoldierAttackSOBase EnemyAttackBaseInstance { get; set; }
    public SoldierRunSOBase EnemyRunBaseInstance { get; set; }

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyRunBaseInstance = Instantiate(EnemyRunBase);

        StateMachine = new SoldierStateMachine();

        IdleState = new SoldierIdleState(this, StateMachine);
        ChaseState = new SoldierChaseState(this, StateMachine);
        AttackState = new SoldierAttackState(this, StateMachine);
        RunState = new SoldierRunState(this, StateMachine);
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
