using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int MaxHealth { get; set; }
    int Health { get; set; }
    int RecoveryPerSeconds { get; set; }

    public event EventHandler OnDie;

    public void TakeDamage();
    public void Recovery();
}
