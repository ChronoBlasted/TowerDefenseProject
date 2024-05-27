using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    public event EventHandler OnDestinationReached;
    public void MoveTo();
}
