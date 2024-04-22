using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private Transform _playerTransform;
    private float _movementSpeed = 1.75f;

    private float _bulletSpeed = 5f;
    private float _timeBetweenShot = 1f;
    private float _distanceToCountExit = 3f;
    private float _timeTillExit = 3f;

    private float _timer;
    private float _exitTimer;
    public AttackState(EnemyBase enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
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
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_timer > _timeBetweenShot)
        {
            _timer = 0f;
            Vector2 dir = (_playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(enemy.BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.velocity = dir * _bulletSpeed;

        }

        if (Vector2.Distance(_playerTransform.position, enemy.transform.position) > _distanceToCountExit)
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

        enemy.MoveEnemy(Vector2.zero);

        _timer += Time.deltaTime;

        if (enemy.IsInRange)
        {
            enemy.StateMachine.ChangeState(enemy.RunState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
