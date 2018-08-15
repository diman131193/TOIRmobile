using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public interface IStateMachine
{
    IState LastState { get; }
    void Load(IState state);
    void Unload(bool loadPrev);
}

public class StateMachine : IStateMachine 
{
    private readonly Stack<IState> stateStack = new Stack<IState>();
    private IState currentState;
    public IState LastState
    {
        get
        {
            if (stateStack.Count == 0)
                return null;
            return stateStack.Peek();
        }
    }

    public void Load(IState state)
    {
        currentState = state;
        stateStack.Push(state);
        state.Load();
    }

    public void Unload(bool loadPrev)
    {
        if (stateStack.Count == 0)
            return;
        var state = stateStack.Pop();
        state.Unload();
        if (currentState == state)
            currentState = null;
        if (loadPrev && LastState != null)
        {
            currentState = LastState;
            currentState.Load();
        }
    }
}
