using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IMove, IAttack
{
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] int strenght = 1;
    public int Strenght { get => strenght; set => strenght = value; }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnAttack;
    public event Action OnDestinationReached;

    [HideInInspector]
    public GameObject currentTarget;


    public Transform sunrise;

    public float journeyTime = 10.0f;
    float startTime;

    private void Start()
    {
        startTime = Time.time;
        sunrise = transform;
        
        OnDestinationReached += Attack;
    }

    public void Attack()
    {
        Destroy(gameObject);
    }

    public void ChooseTarget()
    {

    }

    private void Update()
    {
        MoveTo();
    }

    public void MoveTo()
    {
        Vector3 center = (sunrise.position + currentTarget.transform.position) * 0.5F;

        center -= new Vector3(0, 10, 0);

        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = currentTarget.transform.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == currentTarget)
        {
            OnDestinationReached.Invoke();
        }
    }
}
