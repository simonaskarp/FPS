using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.onDamage.AddListener(OnDamage);
        health.onDeath.AddListener(OnDeath);
    }

    private void OnDamage()
    {
        print("ouch");
    }

    private void OnDeath()
    {
        print("RIP");
    }
}
