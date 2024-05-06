using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackSOBase : ScriptableObject
{
    protected HeavyBase enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, HeavyBase enemy)
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
 
    }

    public virtual void DoPhysicsLogic()
    {

    }

    public virtual void DoAnimationTriggerEventLogic(HeavyBase.AnimationTriggerType triggerType)
    {

    }

    public virtual void ResetValue()
    {

    }
}
