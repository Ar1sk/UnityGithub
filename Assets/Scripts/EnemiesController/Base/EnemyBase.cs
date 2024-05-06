using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public UnityEvent OnDamage;

    public UnityEvent OnDie;

    #region StateMachine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public IdleState IdleState { get; set; }
    public ChaseState ChaseState { get; set; }
    public AttackState AttackState { get; set; }
    public RunState RunState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackArea { get; set; }
    public bool IsInRange { get; set; }
    #endregion

    #region ScriptableObject Variables
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyRunSOBase EnemyRunBase;
    #endregion

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyRunSOBase EnemyRunBaseInstance { get; set; }

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyRunBaseInstance = Instantiate(EnemyRunBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new IdleState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);
        RunState = new RunState(this, StateMachine);
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
        if(currentHealth <= 0f)
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

        if(moveDir > 0f && !IsFacingRight)
        {
            flip();
        }
        else if(moveDir < 0f && IsFacingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        IsFacingRight = !IsFacingRight;

        Vector3  rotation = transform.eulerAngles;
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
