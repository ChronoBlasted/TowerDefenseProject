using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour, IHealth
{
    [SerializeField] private int  PDV, MaxPDV;


    public int MaxHealth { get => MaxPDV; set => MaxPDV = value;}
    public int Health { get => PDV; set => PDV = value; }
    public int RecoveryPerSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action OnDie;

    public void Recovery()
    {
        //throw new NotImplementedException();
    }

    public void TakeDamage(int n)
    {
        Health -= n;
    }

    // Start is called before the first frame update
    void Start()
    { 
           PDV = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Health --;
        }

    }



}
