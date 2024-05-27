using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int Strenght { get; set; }
    float AttackSpeed { get; set; }

    public event EventHandler OnAttack;

    public void Attack();
    public void ChooseTarget();
}
