using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBoxCollider : MonoBehaviour
{
    [SerializeField] Bullet bullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == bullet.currentTarget)
        {
            bullet.BulletHitTarget();
        }
    }
}
