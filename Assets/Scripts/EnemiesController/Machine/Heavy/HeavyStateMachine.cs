using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStateMachine : MonoBehaviour
{
    public HeavyState CurrentEnemyState { get; set; }
    public void Initialize(HeavyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(HeavyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}
