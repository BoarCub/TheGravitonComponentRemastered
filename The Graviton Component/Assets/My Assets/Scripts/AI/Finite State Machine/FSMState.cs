using UnityEngine;
using System.Collections;

public interface FSMState
{
    void Update(FiniteStateMachine fsm, GameObject gameObject);
}
