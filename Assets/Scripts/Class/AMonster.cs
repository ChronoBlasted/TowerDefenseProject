using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMonster : MonoBehaviour, IAttack, IHealth, IMove
{
    public float movementSpeed;
    [SerializeField] private float _MaxHealth, _Health, _RecoveryPerSeconds;
    public Transform target;
    private NavMeshAgent agent;
    
    public float MaxHealth { get => _MaxHealth; set => new int(); }
    public float Health { get => _Health; set => new int(); }
    public float RecoveryPerSeconds { get => _RecoveryPerSeconds; set => new int(); }
    public int Strength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public LayerMask TargetLayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnDie;
    public event Action OnAttack;
    public event Action OnDestinationReached;

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
        Health = MaxHealth;
        target = FindObjectOfType<Nexus>().transform;
        agent = GetComponent<NavMeshAgent>();
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

    }

    public void MoveTo()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        NavMeshPath navMeshPath = new NavMeshPath();

        if (agent.CalculatePath(target.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(target.position);
            agent.speed = movementSpeed;
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
