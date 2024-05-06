using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sniper-Chase-Direct Chase", menuName = "Sniper Logic/Chase Logic/Direct Chase")]
public class SniperChaseDirectToPlayer : SniperChaseSOBase
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
        enemy.MoveEnemy(MoveDirection * _movementSpeed);
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
