using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tower : MonoBehaviour, ISpawner, IAttack
{
    [SerializeField] float attackSpeed;
    [SerializeField] int attackStrenght;
    [SerializeField] Transform canon;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] Bullet towerBullet;

    //TimeStamp
    GameObject currentGameobject;
    Bullet currentBullet;

    //Cache
    List<GameObject> monstersInRange = new List<GameObject>();
    GameObject currentTarget;

    float timeSinceLastShot;

    public GameObject ObjectToSpawn { get => towerBullet.gameObject; set => throw new System.NotImplementedException(); }
    public Vector3 SpawnPosition { get => canon.position; set => canon.position = value; }
    public int Strenght { get => attackStrenght; set => attackStrenght = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }


    public event Action OnAttack;

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
        if (monstersInRange.Count > 0) currentTarget = monstersInRange[0];
        else
        {
            currentTarget = null;
            return;
        }
    }

    private void Update()
    {
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
        Debug.Log(SpawnPosition);
        currentGameobject = Instantiate(ObjectToSpawn, SpawnPosition, canon.rotation);
        currentBullet = currentGameobject.GetComponent<Bullet>();

        currentBullet.Strenght = Strenght;
        currentBullet.currentTarget = currentTarget;
    }

    public void Upgrade()
    {

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

