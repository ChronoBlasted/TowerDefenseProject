using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMonster : MonoBehaviour, IAttack, IHealth, IMove
{
    public float movementSpeed;

    public int MaxHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int RecoveryPerSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Strengh { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public NavMeshAgent agent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnDie;
    public event EventHandler OnAttack;
    public event EventHandler OnDestinationReached;

    private WaveManager waveManager;

    public void Recovery()
    {
        if (Health < MaxHealth)
        {
            Health += RecoveryPerSeconds / 60;
        }
    }

    public void TakeDamage(int n)
    {
        Health -= n;
        if (Health <= 0)
        {
            OnDie.Invoke();
        }
    }

    void Start()
    {
        OnDie += DropResouces;
        OnDie += CheckWaveState;
        waveManager = FindObjectOfType<WaveManager>();
    }

    void FixedUpdate()
    {
        Recovery();
    }

    void DropResouces()
    {

    }

    void CheckWaveState()
    {
        waveManager.CheckNextWave();
        Destroy(this.gameObject);
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public void ChooseTarget()
    {
        //FindObjectsOfType
        /*
        float distance = Vector3.Distance(target.position, transform.position);
        agent.SetDestination(target.position);
        throw new NotImplementedException();*/
    }

    public void MoveTo()
    {
        throw new NotImplementedException();
    }
}
