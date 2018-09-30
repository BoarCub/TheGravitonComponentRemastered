using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GOAPAgent : MonoBehaviour {

    private FiniteStateMachine stateMachine;

    private FiniteStateMachine.FSMState idleState; //Finds a task
    private FiniteStateMachine.FSMState moveState; // Moves to target
    private FiniteStateMachine.FSMState performActionState; // Performs action

    private HashSet<GOAPAction> actionsAvailable;
    private Queue<GOAPAction> currentActions;

    private GOAPImplement dataProvider; //Provides world data and listens to feedback
    private GOAPPlanner planner;

    //Initialization
    void Start ()
    {
        stateMachine = new FiniteStateMachine();
        actionsAvailable = new HashSet<GOAPAction>();
        currentActions = new Queue<GOAPAction>();
        planner = new GOAPPlanner();
        findDataProvider();
        createIdleState();
        createMoveToState();
        createPerformActionState();
        stateMachine.Push(idleState);
        loadActions();
    }

    //Update is called once per frame
    void Update()
    {
        stateMachine.Update(this.gameObject);
    }

    public void addAction(GOAPAction a)
    {
        actionsAvailable.Add(a);
    }

    public GOAPAction getAction(Type action)
    {
        foreach (GOAPAction g in actionsAvailable)
        {
            if (g.GetType().Equals(action))
                return g;
        }
        return null;
    }

    public void removeAction(GOAPAction action)
    {
        actionsAvailable.Remove(action);
    }

    private bool hasActionPlan()
    {
        return currentActions.Count > 0;
    }

    private void createIdleState()
    {
        idleState = (FiniteStateMachine, gameObj) =>
        {
            //Planning

            //Gets world state
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorldState();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoalState();

            //Plan
            Queue<GOAPAction> plan = planner.plan(gameObject, actionsAvailable, worldState, goal);
            if (plan != null)
            {
                //Already Have Plan
                currentActions = plan;
                dataProvider.planFound(goal, plan);

                FiniteStateMachine.Pop(); //switch to PerformAction
                FiniteStateMachine.Push(performActionState);
            }
            else
            {
                //No Plan
                dataProvider.planFailed(goal);
                FiniteStateMachine.Pop(); //switch to idle
                FiniteStateMachine.Push(idleState);
            }
        };
    }

    private void createMoveToState()
    {
        moveState = (FiniteStateMachine, gameObj) =>
        {

            //move gameobject

            GOAPAction action = currentActions.Peek();
            if (action.mustBeInRange() && action.target == null)
            {
                FiniteStateMachine.Pop(); //move
                FiniteStateMachine.Pop(); //perform
                FiniteStateMachine.Push(idleState);
                return;
            }

            //agent moves itself
            if (dataProvider.moveAgent(action))
            {
                FiniteStateMachine.Pop();
            }

            //if (movable == null)
            //{
            //    FiniteStateMachine.Pop(); //move
            //    FiniteStateMachine.Pop(); //perform
            //    FiniteStateMachine.Push(idleState);
            //    return;
            //}

            //Variables
            var thisEnemy = gameObj.GetComponent<Enemy>();

            Rigidbody2D rb = gameObj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, Mathf.Sign(action.target.transform.position.x - rb.position.x) * thisEnemy.maxSpeed, thisEnemy.runForce * Time.deltaTime), rb.velocity.y);

            if (gameObj.transform.position.Equals(action.target.transform.position.x))
            {
                //at target position
                action.setInRange(true);
                FiniteStateMachine.Pop();
            }
        };
    }

    private void createPerformActionState()
    {
        performActionState = (FiniteStateMachine, gameObj) =>
        {
            //performs action

            if (!hasActionPlan())
            {
                //no actions
                FiniteStateMachine.Pop();
                FiniteStateMachine.Push(idleState);
                dataProvider.actionsFinished();
                return;
            }

            GOAPAction action = currentActions.Peek();
            if (action.isDone())
            {
                //action done
                currentActions.Dequeue();
            }

            if (hasActionPlan())
            {
                //perform next action
                action = currentActions.Peek();
                bool inRange = action.mustBeInRange() ? action.isInRange() : true;

                if (inRange)
                {
                    //in range
                    bool success = action.perform(gameObj);

                    if (!success)
                    {
                        //action failed
                        FiniteStateMachine.Pop();
                        FiniteStateMachine.Push(idleState);
                        dataProvider.planAborted(action);
                    }
                }
                else
                {
                    //move there first
                    FiniteStateMachine.Push(moveState);
                }
            }
            else
            {
                //no actions left
                FiniteStateMachine.Pop();
                FiniteStateMachine.Push(idleState);
                dataProvider.actionsFinished();
            }
        };
    }

    private void findDataProvider()
    {
        foreach (Component component in gameObject.GetComponents(typeof(Component)))
        {
            if (typeof(GOAPImplement).IsAssignableFrom(component.GetType()))
            {
                dataProvider = (GOAPImplement)component;
                return;
            }
        }
    }

    private void loadActions()
    {
        GOAPAction[] actions = gameObject.GetComponents<GOAPAction>();
        foreach(GOAPAction a in actions)
        {
            actionsAvailable.Add(a);
        }
    }

}