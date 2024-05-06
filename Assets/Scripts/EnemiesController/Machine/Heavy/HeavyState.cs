using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyState : MonoBehaviour
{
    protected HeavyBase enemy;
    protected HeavyStateMachine enemyStateMachine;

    public HeavyState(HeavyBase enemy, HeavyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void AnimationTriggerEvent(HeavyBase.AnimationTriggerType triggerType)
    {

    }
}
