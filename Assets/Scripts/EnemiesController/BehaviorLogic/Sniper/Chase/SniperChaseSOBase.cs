using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperChaseSOBase : ScriptableObject
{
    protected SniperBase enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, SniperBase enemy)
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
        if (enemy.IsWithinAttackArea)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    public virtual void DoPhysicsLogic()
    {

    }

    public virtual void DoAnimationTriggerEventLogic(SniperBase.AnimationTriggerType triggerType)
    {

    }

    public virtual void ResetValue()
    {

    }
}
