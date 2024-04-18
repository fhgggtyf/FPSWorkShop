using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    EnemyAI myEnemy;
    float actualTimeBetweenShots = 0;


    // When we call the constructor, we save
    // a reference to our enemy's AI
    public AttackState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    // Here goes all the functionality that we want
    // what the enemy does when he is in this
    // state.
    public void UpdateState()
    {
        myEnemy.myLight.material.color = Color.red;
        actualTimeBetweenShots += Time.deltaTime;


        RaycastHit hit;
        if (Physics.Raycast(new Vector3(myEnemy.transform.position.x, 5f, myEnemy.transform.position.z), myEnemy.transform.forward, out hit, 100f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log(hit.collider.name); 
                Vector3 lookDirection = hit.collider.transform.position - myEnemy.transform.position;

                //We rotate around the Y axis
                myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDirection.x, 0, lookDirection.z));

                //Turn to shoot
                if (actualTimeBetweenShots > myEnemy.timeBetweenShots)
                {
                    actualTimeBetweenShots = 0;
                    hit.collider.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().Hit(myEnemy.damageForce);
                    myEnemy.fireSound.Play();
                }
            }
            else
            {
                GoToAlertState();
            }
        }
        //We always look at the player

    }

    //If the Player has hit us we do nothing
    public void Impact() { }


    //We are in this state so we never call it
    public void GoToAttackState() { }

    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }

    //The player is already in our trigger
    public void OnTriggerEnter(Collider col) { }

    //We rotate the enemy to look at the Player while attacking him/her
    public void OnTriggerStay(Collider col) { }

    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();   
    }

}