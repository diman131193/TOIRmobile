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

    public void Unload(bool backButton)
    {
        if (stateStack.Count == 0)
            return;
        if (backButton && (stateStack.Count == 0 || currentState is StartScreen))
        {
            AppQuit();
        }
        var state = stateStack.Peek();
        state.Unload();
        if (currentState == state)
            currentState = null;
        if (backButton && stateStack.Count > 1)
        {
            stateStack.Pop();
            if (LastState is LoadState)
                stateStack.Pop();
            currentState = LastState;
            currentState.Load();
        }
    }

    private void AppQuit()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.IMH.TOIR").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
        else
        {
            Application.Quit();
        }
    }
}
