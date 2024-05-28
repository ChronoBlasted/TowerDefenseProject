using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour, IHealth
{
    public int MaxHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int RecoveryPerSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event EventHandler OnDie;

    public void Recovery()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damages)
    {

        Health -= damages;
        //throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
