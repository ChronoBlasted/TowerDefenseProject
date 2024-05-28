using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IMove
{
    NavMeshAgent agent { get; set; }
    
    public event EventHandler OnDestinationReached;
    public void MoveTo();
}
