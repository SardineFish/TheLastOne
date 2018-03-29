using UnityEngine;
using System.Collections;

public interface IStateMachine
{
    IState CurrentState { get; }

    bool ChangeState(IState state);
}
