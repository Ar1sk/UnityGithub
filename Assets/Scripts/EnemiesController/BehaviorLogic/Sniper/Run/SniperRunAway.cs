using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sniper-Run-From Player", menuName = "Sniper Logic/Run Logic/Run From Player")]
public class SniperRunAway : SniperRunSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;

    public override void DoAnimationTriggerEventLogic(SniperBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 MoveDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(-MoveDirection * _movementSpeed);

        if (enemy.IsAggroed)
        {
            new WaitForSeconds(2f);
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, SniperBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValue()
    {
        base.ResetValue();
    }
}
