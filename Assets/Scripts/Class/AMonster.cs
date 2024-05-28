using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMonster : MonoBehaviour, IAttack, IHealth, IMove
{
    public float movementSpeed;
    [SerializeField] private float _MaxHealth, _Health, _RecoveryPerSeconds;
    [SerializeField] private int _Strength;
    private Nexus nexus;
    private NavMeshAgent agent;
    public int coinReward;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    public float Health { get => _Health; set => _Health = value; }
    public float RecoveryPerSeconds { get => _RecoveryPerSeconds; set => _RecoveryPerSeconds = value; }
    public int Strength { get => _Strength; set => Strength = value; }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public LayerMask TargetLayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnDie;
    public event Action OnAttack;
    public event Action OnDestinationReached;

    event EventHandler IAttack.OnAttack
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    public void Recovery()
    {
        if (Health < MaxHealth)
        {
            Health += RecoveryPerSeconds / 60;
        }
    }

    public void TakeDamage(float n)
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
        Health = MaxHealth;
        OnDestinationReached += Attack;
        agent = GetComponent<NavMeshAgent>();
        ChooseTarget();
    }

    void FixedUpdate()
    {
        Recovery();
    }

    void Update()
    {
        MoveTo();
    }

    void DropResouces()
    {
        ResourceManager.Instance.AddResources(coinReward);
    }

    void CheckWaveState()
    {
        WaveManager.Instance.CheckNextWave();
        Destroy(this.gameObject);

        Debug.Log("ded");
    }

    public void Attack()
    {
        nexus.TakeDamage(Strength);
        TakeDamage(MaxHealth);
    }

    public void ChooseTarget()
    {
        nexus = FindObjectOfType<Nexus>();
    }

    public void MoveTo()
    {
        float distance = Vector3.Distance(nexus.transform.position, transform.position);
        NavMeshPath navMeshPath = new NavMeshPath();

        if (agent.CalculatePath(nexus.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(nexus.transform.position);
            agent.speed = movementSpeed;
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (nexus.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnDestinationReached.Invoke();
        }
    }
}
