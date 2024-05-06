using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heavy-Chase-Direct Chase", menuName = "Heavy Logic/Chase Logic/Direct Chase")]
public class HeavyChaseDirectToPlayer : HeavyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;

    public override void DoAnimationTriggerEventLogic(HeavyBase.AnimationTriggerType triggerType)
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

    public override void Initialize(GameObject gameObject, HeavyBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValue()
    {
        base.ResetValue();
    }
}
