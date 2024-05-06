using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperStateMachine
{
    public SniperState CurrentEnemyState { get; set; }
    public void Initialize(SniperState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(SniperState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}
