using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    EnemyAI myEnemy;
    private int newWayPoint = 0;


    // When we call the constructor, we save
    // a reference to our enemy's AI
    public PatrolState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    // Here goes all the functionality that we want
    // what the enemy does when he is in this
    // state.
    public void UpdateState()
    {
        Debug.Log(1);

        myEnemy.myLight.material.color = Color.green;

        myEnemy.navMeshAgent.destination = myEnemy.wayPoints[newWayPoint].position;

        if (myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
        {
            newWayPoint = (newWayPoint + 1) % myEnemy.wayPoints.Length;

        }
    }

    public void Impact()
    {

        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        GoToAttackState();

    }

    public void GoToAlertState()
    {

        myEnemy.navMeshAgent.isStopped=true;
        myEnemy.currentState = myEnemy.alertState;
    }




    // In this state the player is already inside, so we will ignore it.
    public void OnTriggerEnter(Collider col) {
        GoToAlertState();
    }

    // We will orient the enemy always looking at the
    // player while we attack him/her
    public void OnTriggerStay(Collider col)
    {
        //if (col.tag == "Player")
        //{
        //    // We always look at the player.
        //    Vector3 lookDirection = col.transform.position -
        //                        myEnemy.transform.position;

        //    // Rotating only on the Y axis
        //    myEnemy.transform.rotation =
        //        Quaternion.FromToRotation(Vector3.forward,
        //                                    new Vector3(lookDirection.x, 0, lookDirection.z));
        //}
    }

    // If they player is outside the enemy radius, the enemy changes to Idle State.
    public void OnTriggerExit(Collider col)
    {
    }

    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    public void GoToPatrolState()
    {
    }
}
