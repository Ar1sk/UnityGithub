using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine
{
    public SoldierState CurrentEnemyState { get; set; }
    public void Initialize(SoldierState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(SoldierState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}
