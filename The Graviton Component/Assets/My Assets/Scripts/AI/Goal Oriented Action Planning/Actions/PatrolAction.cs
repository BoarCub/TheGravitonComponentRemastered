using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : GOAPAction {

    public Transform RaycastStartingPositionFeet;
    public Transform RaycastStartingPositionArm;

    public float RaycastRange = 0.05f;

    private bool patrolCompleted = false;
    private Transform targetPosition;

    public PatrolAction()
    {
        addEffect("hasPatrolled", true);
    }

    public override void reset()
    {
        targetPosition = null;
        patrolCompleted = false;
    }

    public override bool isDone()
    {
        return patrolCompleted;
    }

    public override bool mustBeInRange()
    {
        return false;
    }

    public override bool checkPreconditionProcedurally(GameObject agent)
    {

        return true;

    }

    public override bool perform(GameObject agent)
    {
        Enemy enemyScript = agent.GetComponent<Enemy>();
        Rigidbody2D rb = agent.GetComponent<Rigidbody2D>();

        RaycastHit2D raycastFeet = Physics2D.Raycast(RaycastStartingPositionFeet.position, rb.transform.localScale.x*Vector2.right);

        //Raycast Logic
        if(raycastFeet.collider != null)
        {

            String raycastTagFeet = raycastFeet.collider.gameObject.tag;

            if(raycastFeet.distance <= RaycastRange)
            {
                if (raycastTagFeet == "Player")
                {
                    //Nothing Happens Here For Now
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                    enemyScript.Flip();
                    return false;
                }
            }

        }

        RaycastHit2D raycastArm = Physics2D.Raycast(RaycastStartingPositionArm.position, rb.transform.localScale.x * Vector2.right);

        //Raycast Logic
        if (raycastArm.collider != null)
        {

            String raycastTagArm = raycastArm.collider.gameObject.tag;

            if (raycastArm.distance <= RaycastRange)
            {
                if (raycastTagArm == "Player")
                {
                    //Nothing Happens Here For Now
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                    enemyScript.Flip();
                    return false;
                }
            }

        }

        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, rb.transform.localScale.x * enemyScript.maxSpeed, enemyScript.runForce * Time.deltaTime), rb.velocity.y);
        return true;
    }

}
