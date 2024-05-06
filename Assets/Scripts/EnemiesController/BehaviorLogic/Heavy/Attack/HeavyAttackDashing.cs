using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heavy-Attack Dashing", menuName = "Heavy Logic/Attack Logic/Dashing")]
public class HeavyAttackDashing : HeavyAttackSOBase
{
    [SerializeField] private Rigidbody2D BulletPrefab;

    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _timeBetweenShot = 1f;
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _timeTillExit = 3f;

    private float _timer;
    private float _exitTimer;
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

        if (_timer > _timeBetweenShot)
        {
            _timer = 0f;
            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.velocity = dir * _bulletSpeed;

        }

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

        enemy.MoveEnemy(Vector2.zero);

        _timer += Time.deltaTime;
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
