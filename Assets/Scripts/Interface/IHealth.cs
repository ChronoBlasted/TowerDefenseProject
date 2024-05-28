using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float MaxHealth { get; set; }
    float Health { get; set; }
    float RecoveryPerSeconds { get; set; }

    public event Action OnDie;

    public void TakeDamage(int n);
    public void Recovery();
}
