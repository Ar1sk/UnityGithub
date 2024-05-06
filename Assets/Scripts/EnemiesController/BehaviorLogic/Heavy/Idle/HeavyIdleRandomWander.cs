using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Heavy Logic/Idle Logic/Random Wander")]
public class HeavyIdleRandomWander : HeavyIdleSOBase
{
    [SerializeField] private float MovementSpeed = 1f;
    private float minX = 1f;
    private float maxX = 5f;

    private float minY = 1f;
    private float maxY = 5f;

    private Vector3 _targetpos;
    private Vector3 _direction;

    public override void DoAnimationTriggerEventLogic(HeavyBase.AnimationTriggerType triggerType)
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

    public override void Initialize(GameObject gameObject, HeavyBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValue()
    {
        base.ResetValue();
    }

    private Vector3 GetRandomPointInCircle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY);
    }
}
