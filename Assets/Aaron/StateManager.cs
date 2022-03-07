using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public StateBase currentState;

    public void ChangeState(StateBase newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        if (newState != null)
        {
            newState.Enter();
            currentState = newState;
        }
    }

    private void Update()
    {
        currentState.Execute();
    }
}