using System;
using UnityEngine;

public class Box : MonoBehaviour
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
        print("moves a box a little bit");
    }

    private void OnDeath()
    {
        print("spawns many rigidbody pieces");
    }
}
