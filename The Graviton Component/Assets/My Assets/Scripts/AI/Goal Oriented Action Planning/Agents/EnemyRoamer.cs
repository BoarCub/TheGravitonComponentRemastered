using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : EnemyAgent {
	//Only Goal Is To Roam (Patrol In This Case)
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("hasPatrolled", true));
        return goal;
    }

}
