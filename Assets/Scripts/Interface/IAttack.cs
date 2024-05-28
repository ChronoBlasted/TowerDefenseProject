using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int Strengh { get; set; }
    float AttackSpeed { get; set; }

    public event EventHandler OnAttack;

    LayerMask TargetLayers { get; set; }

    public void Attack();
    public void ChooseTarget();
}
