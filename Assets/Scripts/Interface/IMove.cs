using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    public event Action OnDestinationReached;
    public void MoveTo();
}
