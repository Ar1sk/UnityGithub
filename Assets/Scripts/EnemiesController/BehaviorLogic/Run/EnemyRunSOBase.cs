using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {

    }

    public virtual void DoExitLogic()
    {
        ResetValue();
    }

    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.IsAggroed)
        {
            new WaitForSeconds(2f);
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public virtual void DoPhysicsLogic()
    {

    }

    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {

    }

    public virtual void ResetValue()
    {

    }
}
