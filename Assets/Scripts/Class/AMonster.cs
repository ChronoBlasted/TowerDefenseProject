using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class AMonster : MonoBehaviour, IAttack, IHealth, IMove
{
    public float movementSpeed;
    [SerializeField] private float _MaxHealth, _Health, _RecoveryPerSeconds;
    [SerializeField] private int _Strength;
    private Nexus nexus;
    public ParticleSystem deathParticleSystem, attackParticleSystem;
    public Slider healthSlider;
    private bool dieEventHasEnded;

    public int amountReward;
    public RESOURCETYPE typeReward;

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
        healthSlider.value = Health;
        if (Health <= 0)
        {
            OnDie.Invoke();
            StartCoroutine(DeathEffectCoroutine());
        }
    }

    void Start()
    {
        dieEventHasEnded = false;
        OnDie += DropResouces;
        OnDie += CheckWaveState;
        OnDie += DeathEffect;

        OnAttack += DropResouces;
        OnAttack += CheckWaveState;
        OnAttack += AttackEffect;

        Health = MaxHealth;
        OnDestinationReached += Attack;
        ChooseTarget();

        healthSlider.maxValue = MaxHealth;
        healthSlider.value = Health;
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
        ResourceManager.Instance.AddResources(new Dictionary<RESOURCETYPE, int>
        {
            { typeReward, amountReward }
        });
    }

    void CheckWaveState()
    {
        WaveManager.Instance.CheckNextWave();
    }

    void DeathEffect()
    {
        Instantiate<ParticleSystem>(deathParticleSystem, transform.position, transform.rotation);
        WaveManager.Instance.enemiesList.Remove(this.gameObject);
        dieEventHasEnded = true;
    }

    void AttackEffect()
    {
        WaveManager.Instance.enemiesList.Remove(this.gameObject);
        dieEventHasEnded = true;
    }
    IEnumerator DeathEffectCoroutine()
    {
        while (!dieEventHasEnded)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void Attack()
    {
        nexus.TakeDamage(Strength);
        Instantiate<ParticleSystem>(attackParticleSystem, transform.position, transform.rotation);
        OnAttack.Invoke();
        StartCoroutine(DeathEffectCoroutine());
    }

    public void ChooseTarget()
    {
        nexus = FindObjectOfType<Nexus>();
    }

    public void MoveTo()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, nexus.transform.position, movementSpeed * Time.deltaTime);
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
