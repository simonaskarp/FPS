using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Health health;
    NavMeshAgent agent;
    public Transform target;

    private void Awake()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();

        health.onDamage.AddListener(OnDamage);
        health.onDeath.AddListener(OnDeath);
    }

    private void Update()
    {
        agent.destination = target.position;
    }

    private void OnDamage()
    {
        //print("ouch");
    }

    private void OnDeath()
    {
        //print("RIP");
    }
}
