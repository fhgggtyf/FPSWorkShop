using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    public AudioSource fireSound;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public GameObject child;

    [HideInInspector]public Renderer myLight;
    public float life = 100;
    public float timeBetweenShots = 1.0f;
    public float damageForce = 10;
    public float rotationTime = 3.0f;
    public float shotHeight = 0.5f;

    public Transform[] wayPoints = new Transform[10];

    void Start()
    {
        myLight = child.GetComponent<Renderer>();

        // AI States.
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        patrolState = new PatrolState(this);

        // Start patrolling
        currentState = patrolState;

        //Keep a NavMesh reference
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        // Since our states don't inherit from
        // MonoBehaviour, its update is not called
        // automatically, and we'll take care of it
        // us to call it every frame.
        currentState.UpdateState();

        if (life <= 0) this.gameObject.SetActive(false);

    }

    public void Hit(float damage)
    {
        life -= damage;
        currentState.Impact();
        Debug.Log("Enemy hit:" + life);
    }

    // Since our states don't inherit from
    // MonoBehaviour, we'll have to let them know
    // when something enters, stays,  or leaves our
    // trigger.
    void OnTriggerEnter(Collider col)
    {
        currentState.OnTriggerEnter(col);
    }

    void OnTriggerStay(Collider col)
    {
        currentState.OnTriggerStay(col);
    }

    void OnTriggerExit(Collider col)
    {
        currentState.OnTriggerExit(col);
    }
}