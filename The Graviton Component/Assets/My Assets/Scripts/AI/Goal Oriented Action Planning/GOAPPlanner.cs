using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner{

	//Plans Sequence of Actions to Fulfill Goal
    public Queue<GOAPAction> plan(GameObject agent, HashSet<GOAPAction> actionsAvailable, HashSet<KeyValuePair<string, object>> worldState, HashSet<KeyValuePair<string, object>> goal)
    {
        //reset actions
        foreach(GOAPAction a in actionsAvailable)
        {
            a.doReset();
        }

        //check if action can run
        HashSet<GOAPAction> usableActions = new HashSet<GOAPAction>();
        foreach(GOAPAction a in actionsAvailable)
        {
            if (a.checkPreconditionProcedurally(agent))
                usableActions.Add(a);
        }

        //actions that can run stored in usableActions

        //build tree
        List<Node> leaves = new List<Node>();

        //builds graph
        Node start = new Node(null, 0, worldState, null);
        bool success = buildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            //could not get plan
            return null;
        }

        //get cheapest leaf
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.runningCost < cheapest.runningCost)
                    cheapest = leaf;
            }
        }

        //gets node and tracks to parent
        List<GOAPAction> result = new List<GOAPAction>();
        Node node = cheapest;
        while (node != null)
        {
            if (node.action != null)
            {
                result.Insert(0, node.action); //Insert Action
            }
            node = node.parent;
        }
        //action list in correct order
        Queue<GOAPAction> queue = new Queue<GOAPAction>();
        foreach(GOAPAction a in result)
        {
            queue.Enqueue(a);
        }

        //plan found
        return queue;
    }

    private bool buildGraph(Node parent, List<Node> leaves, HashSet<GOAPAction> usableActions, HashSet<KeyValuePair<string, object>> goal)
    {
        bool found = false;

        //iterate through each action
        foreach(GOAPAction action in usableActions)
        {
            if(inState(action.Preconditions, parent.state))
            {
                //apply effect to parent state
                HashSet<KeyValuePair<string, object>> currentState = populateState(parent.state, action.Effect);
                Node node = new Node(parent, parent.runningCost + action.cost, currentState, action);

                if(inState(goal, currentState))
                {
                    //Found Solution
                    leaves.Add(node);
                    found = true;
                } else
                {
                    //Test Remaining Actions and Branches
                    HashSet<GOAPAction> subset = actionSubset(usableActions, action);
                    bool foundVar = buildGraph(node, leaves, subset, goal);
                    if (foundVar)
                        found = true;
                }
            }
        }
        return found;
    }

    //Create a subset of the actions
    private HashSet<GOAPAction> actionSubset(HashSet<GOAPAction> actions, GOAPAction removeMe)
    {
        HashSet<GOAPAction> subset = new HashSet<GOAPAction>();
        foreach(GOAPAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

    //Check if items are in state
    private bool inState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
    {
        bool allMatch = true;
        foreach(KeyValuePair<string, object> t in test)
        {
            bool match = false;
            foreach(KeyValuePair<string, object> s in state)
            {
                if (s.Equals(t))
                {
                    match = true;
                    break;
                }
            } if (!match)
                allMatch = false;
        }
        return allMatch;
    }

    //Apply stateChange to currentState
    private HashSet<KeyValuePair<string, object>> populateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
    {
        HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();
        //copy KVPs as new objects
        foreach (KeyValuePair<string,object> s in currentState)
        {
            state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
        }

        foreach(KeyValuePair<string, object> change in stateChange)
        {
            //update value
            bool exists = false;

            foreach(KeyValuePair<string, object> s in state)
            {
                if(s.Equals(change))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
                KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
                state.Add(updated);
            }
            else
            {
                state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }
        }
        return state;
    }

    //Building Graph
    private class Node
    {
        public Node parent;
        public float runningCost;
        public HashSet<KeyValuePair<string, object>> state;
        public GOAPAction action;

        public Node(Node parent, float runningCost, HashSet<KeyValuePair<string, object>> state, GOAPAction action)
        {
            this.parent = parent;
            this.runningCost = runningCost;
            this.state = state;
            this.action = action;
        }
    }

}