using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Agents implement this interface
public interface GOAPImplement
{
    //Starting State
    HashSet<KeyValuePair<string, object>> getWorldState();

    //New Goal
    HashSet<KeyValuePair<string, object>> createGoalState();

    //Try another goal
    void planFailed(HashSet<KeyValuePair<string, object>> failedGoal);

    //Plan found
    void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions);

    //All actions complete
    void actionsFinished();

    //Plan Aborted
    void planAborted(GOAPAction aborter);

    //Called During Update
    bool moveAgent(GOAPAction nextAction);
}
