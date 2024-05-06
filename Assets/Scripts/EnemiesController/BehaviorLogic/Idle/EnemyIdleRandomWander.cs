using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float MovementRange = 5f;
    [SerializeField] private float MovementSpeed = 1f;

    private Vector3 _targetpos;
    private Vector3 _direction;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _targetpos = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _direction = (_targetpos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * MovementSpeed);

        if ((enemy.transform.position - _targetpos).sqrMagnitude < 0.01f)
        {
            _targetpos = GetRandomPointInCircle();
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValue()
    {
        base.ResetValue();
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * MovementRange;
    }
}
