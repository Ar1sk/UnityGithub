using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperState
{
    protected SniperBase enemy;
    protected SniperStateMachine enemyStateMachine;

    public SniperState(SniperBase enemy, SniperStateMachine enemyStateMachine)
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

    public virtual void AnimationTriggerEvent(SniperBase.AnimationTriggerType triggerType)
    {

    }
}
