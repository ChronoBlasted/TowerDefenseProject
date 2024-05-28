using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IMove, IAttack
{
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float sunriseAmount = 10;
    [SerializeField] GameObject bulletRenderer;
    [SerializeField] float normalSpeedAtDistance = 2;
    [SerializeField] AnimationCurve easeCurve;


    [HideInInspector]
    public GameObject currentTarget;

    public int Strenght { get => strenght; set => strenght = value; }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnAttack;
    public event Action OnDestinationReached;

    int strenght;
    Vector3 sunrisePosition;
    Vector3 lastPosition;
    float startTime;
    List<GameObject> allMonsterToDamage = new List<GameObject>();


    private void Awake()
    {
        bulletRenderer.SetActive(false);
    }

    private void Start()
    {
        startTime = Time.time;

        sunrisePosition = transform.position;
        lastPosition = transform.position;

        OnDestinationReached += Attack;
    }

    public void Attack()
    {
        foreach (var item in allMonsterToDamage)
        {
            //Take Damage
            Debug.Log("Take Damage");
        }

        // Do fx

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
        var distance = (currentTarget.transform.position - sunrisePosition).magnitude;

        Vector3 center = (sunrisePosition + currentTarget.transform.position) * 0.5F;

        center -= new Vector3(0, sunriseAmount, 0);

        Vector3 riseRelCenter = sunrisePosition - center;
        Vector3 setRelCenter = currentTarget.transform.position - center;

        float fracComplete = (Time.time - startTime) / (bulletSpeed * (distance / normalSpeedAtDistance));
        fracComplete = Mathf.Clamp01(fracComplete);
        fracComplete = easeCurve.Evaluate(fracComplete);

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        bulletRenderer.transform.forward = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;

        if (!bulletRenderer.activeSelf) bulletRenderer.SetActive(true);
    }

    public void BulletHitTarget()
    {
        OnDestinationReached.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            allMonsterToDamage.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            allMonsterToDamage.Remove(other.gameObject);
        }
    }
}
