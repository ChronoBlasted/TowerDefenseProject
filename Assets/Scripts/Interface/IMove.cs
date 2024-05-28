using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IMove
{
    public event Action OnDestinationReached;
    public void MoveTo();
}
