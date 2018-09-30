using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour {

    //Stores Preconditions and Effects
    private HashSet<KeyValuePair<string, object>> preconditions;
    private HashSet<KeyValuePair<string, object>> effects;

    private bool withinRange = false;

    //Cost of actions
    public float cost = 1f;

    //Object action is performed on
    public GameObject target;

    public GOAPAction()
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void doReset()
    {
        withinRange = false;
        target = null;
        reset();
    }

    //Resets variables prior to planning
    public abstract void reset();

    //Is action finished?
    public abstract bool isDone();

    //Checks if action can run procedurally
    public abstract bool checkPreconditionProcedurally(GameObject agent);

    //Runs action and returns its success
    public abstract bool perform(GameObject agent);

    //Must be in range?
    public abstract bool mustBeInRange();

    //In range of target?
    public bool isInRange()
    {
        return withinRange;
    }

    public void setInRange(bool withinRange)
    {
        this.withinRange = withinRange;
    }

    public void addPrecondition(string key, object value)
    {
        preconditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void removePrecondition(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in preconditions)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
    }

    public void addEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }

    public void removeEffect(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in effects)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
    }

    public HashSet<KeyValuePair<string, object>> Preconditions
    {
        get
        {
            return preconditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effect
    {
        get
        {
            return effects;
        }
    }
}