using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState
{
    protected SoldierBase enemy;
    protected SoldierStateMachine enemyStateMachine;

    public SoldierState(SoldierBase enemy, SoldierStateMachine enemyStateMachine)
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

    public virtual void AnimationTriggerEvent(SoldierBase.AnimationTriggerType triggerType)
    {

    }
}
