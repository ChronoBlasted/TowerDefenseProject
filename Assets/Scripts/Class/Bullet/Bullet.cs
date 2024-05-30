using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    MONO,
    AREA
}

public abstract class Bullet : MonoBehaviour, IMove, IAttack
{
    [SerializeField] TargetType targetType;
    [SerializeField] float sunriseAmount = 1;
    [SerializeField] GameObject bulletRenderer;

    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float normalSpeedAtDistance = 2;
    [SerializeField] AnimationCurve easeCurve;

    [SerializeField] Transform dieFXTransform;
    [SerializeField] ParticleSystem onDieFX;

    [HideInInspector]
    public GameObject currentTarget;

    public int Strength { get => strength; set => strength = value; }
    public float AttackSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public LayerMask TargetLayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnAttack;
    public event Action OnDestinationReached;

    int strength;
    Vector3 sunrisePosition;
    Vector3 lastPosition;
    float startTime;
    public List<AMonster> allMonsterToDamage = new List<AMonster>();

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
        switch (targetType)
        {
            case TargetType.MONO:
                allMonsterToDamage[0].TakeDamage(Strength);
                break;
            case TargetType.AREA:
                foreach (var monster in allMonsterToDamage)
                {
                    monster.TakeDamage(Strength);
                }
                break;
        }

        DieFeedbacks();
    }

    private void DieFeedbacks()
    {
        if (gameObject == null) return;
        Instantiate(onDieFX, dieFXTransform.transform.position, Quaternion.identity,null);

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
        if (currentTarget == null)
        {
            DieFeedbacks();

            return;
        }

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
            allMonsterToDamage.Add(other.GetComponent<AMonster>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            allMonsterToDamage.Remove(other.GetComponent<AMonster>());
        }
    }
}
