using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMonsterBulletCollider : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            bullet.allMonsterToDamage.Add(other.GetComponent<AMonster>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            bullet.allMonsterToDamage.Remove(other.GetComponent<AMonster>());
        }
    }
}
