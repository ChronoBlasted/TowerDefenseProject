using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tower : MonoBehaviour, ISpawner, IAttack
{
    public RESOURCETYPE costResourceType;
    public int price;
    public bool isTowerBuilt;

    [SerializeField] float attackSpeed;
    [SerializeField] int attackStrength;
    [SerializeField] Transform canon;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] Bullet towerBullet;
    [SerializeField] GameObject rangeZone;
    [SerializeField] ParticleSystem popingParticle;


    //TimeStamp
    GameObject currentGameobject;
    Bullet currentBullet;

    //Cache
    List<GameObject> monstersInRange = new List<GameObject>();
    GameObject currentTarget;

    float timeSinceLastShot;

    public GameObject ObjectToSpawn { get => towerBullet.gameObject; set => throw new System.NotImplementedException(); }
    public Vector3 SpawnPosition { get => canon.position; set => canon.position = value; }
    public int Strength { get => attackStrength; set => attackStrength = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public LayerMask TargetLayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnAttack;

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

    private void Start()
    {
        OnAttack += Spawn;
    }

    public void Attack()
    {
        OnAttack.Invoke();
    }

    public void ChooseTarget()
    {
        if (monstersInRange.Count > 0)
        {
            currentTarget = monstersInRange[0];

            currentTarget.GetComponent<AMonster>().OnDie += () =>
            {
                monstersInRange.Remove(currentTarget.gameObject);
                ChooseTarget();
            };
        }
        else
        {
            currentTarget = null;
            return;
        }
    }



    private void Update()
    {
        if (isTowerBuilt == false) return;

        timeSinceLastShot += Time.deltaTime;

        if (currentTarget == null) ChooseTarget();
        if (currentTarget != null)
        {
            transform.LookAt(currentTarget.transform);

            if (timeSinceLastShot >= AttackSpeed)
            {
                Attack();
                timeSinceLastShot = 0f;
            }
        }
    }

    public void Spawn()
    {
        currentGameobject = Instantiate(ObjectToSpawn, SpawnPosition, canon.rotation);
        currentBullet = currentGameobject.GetComponent<Bullet>();

        currentBullet.Strength = Strength;
        currentBullet.currentTarget = currentTarget;
    }

    public void Upgrade()
    {

    }

    public void OnPose()
    {
        popingParticle.Play();
        Instantiate(popingParticle, transform);
    }

    #region event

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            monstersInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            monstersInRange.Remove(other.gameObject);

            if (currentTarget == other.gameObject) ChooseTarget();
        }
    }
    #endregion
}


