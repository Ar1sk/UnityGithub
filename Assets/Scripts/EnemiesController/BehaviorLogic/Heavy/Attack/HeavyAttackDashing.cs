using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heavy-Attack Dashing", menuName = "Heavy Logic/Attack Logic/Dashing")]
public class HeavyAttackDashing : HeavyAttackSOBase
{
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _timeTillExit = 3f;
    [SerializeField] private float detectionRange = 5f; 
    [SerializeField] private float dashSpeed = 10f; 
    [SerializeField] private float dashDuration = 0.5f; 
    [SerializeField] private float dashCooldown = 2f; 

    private float _exitTimer;
    private bool isDashing = false;
    private bool isDashOnCooldown = false;
    private float dashStartTime;
    private float dashCooldownTimer;
    private Vector2 dashDirection;
    private HeavyBase enemy;
    private Transform playerTransform;
    public override void DoAnimationTriggerEventLogic(HeavyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _exitTimer = 0f;
        isDashing = false;
        isDashOnCooldown = false;
        dashCooldownTimer = 0f;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (isDashing)
        {
            ContinueDashing();
        }
        else
        {
            HandleStateTransition();
            DetectAndStartDash();
            enemy.MoveEnemy(Vector2.zero);
        }

        if (isDashOnCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                isDashOnCooldown = false;
            }
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, HeavyBase enemy)
    {
        base.Initialize(gameObject, enemy);
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void ResetValue()
    {
        base.ResetValue();
        _exitTimer = 0f;
        isDashing = false;
        isDashOnCooldown = false;
        dashCooldownTimer = 0f;
    }

    private void HandleStateTransition()
    {
        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }
    }

    private void DetectAndStartDash()
    {
        if (!isDashOnCooldown && Vector2.Distance(playerTransform.position, enemy.transform.position) <= detectionRange)
        {
            StartDashing();
        }
    }

    private void StartDashing()
    {
        isDashing = true;
        dashStartTime = Time.time;
        dashDirection = (playerTransform.position - enemy.transform.position).normalized;
    }

    private void ContinueDashing()
    {
        if (Time.time < dashStartTime + dashDuration)
        {
            enemy.transform.position += (Vector3)(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            isDashing = false;
            isDashOnCooldown = true;
            dashCooldownTimer = 0f;
        }
    }
}
