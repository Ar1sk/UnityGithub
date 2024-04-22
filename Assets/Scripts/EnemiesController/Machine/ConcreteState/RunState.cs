using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : EnemyState
{
    private Transform _playerTransform;
    private float _movementSpeed = 1.75f;

    public RunState(EnemyBase enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Hello World!");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector2 MoveDirection = (_playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(-MoveDirection * _movementSpeed);

        if (enemy.IsAggroed)
        {
            new WaitForSeconds(2f);
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
