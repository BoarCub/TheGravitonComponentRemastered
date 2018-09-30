using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAgent : MonoBehaviour, GOAPImplement {

    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Key-Value Data
    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        //World Data
        worldData.Add(new KeyValuePair<string, object>("distanceFromPlayer", Mathf.Abs(player.GetComponent<Rigidbody2D>().transform.position.x - gameObject.GetComponent<Rigidbody2D>().transform.position.x)));

        return worldData;
    }

    //Used In Subclasses
    public abstract HashSet<KeyValuePair<string, object>> createGoalState();

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {

    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
    {

    }

    public void actionsFinished()
    {

    }

    public void planAborted(GOAPAction aborter)
    {

    }

    public bool moveAgent(GOAPAction action)
    {
        //move to target
        Enemy enemyScript = GetComponent<Enemy>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //flip
        if (action.target.transform.position.x - rb.transform.position.x > 0 && !enemyScript.facingRight)
        {
            enemyScript.Flip();
        }
        else if (action.target.transform.position.x - rb.transform.position.x < 0 && enemyScript.facingRight)
        {
            enemyScript.Flip();
        }

        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, rb.transform.localScale.x * enemyScript.maxSpeed, enemyScript.runForce * Time.deltaTime), rb.velocity.y);

        if (gameObject.transform.position.x.Equals(action.target.transform.position.x))
        {
            action.setInRange(true);
            return true;
        } else
        {
            return false;
        }

    }

}
