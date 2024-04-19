using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    private Vector3 _targetpos;
    private Vector3 _direction;

    public IdleState(EnemyBase enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        //enemy.IdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _targetpos = GetRandomPointInCircle();
        //enemy.IdleBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        //enemy.IdleBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        _direction = (_targetpos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * enemy.MovementSpeed);

        if ((enemy.transform.position - _targetpos).sqrMagnitude < 0.01f)
        {
            _targetpos = GetRandomPointInCircle();
        }
        //enemy.IdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //enemy.IdleBaseInstance.DoPhysicsLogic();
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.MovementRange;
    }
}
