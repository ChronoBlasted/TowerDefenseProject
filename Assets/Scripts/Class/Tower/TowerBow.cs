using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBow : Tower
{
    public Dictionary<RESOURCETYPE, int> _Price = new Dictionary<RESOURCETYPE, int>();
    void Start()
    {
        _Price.Add(RESOURCETYPE.GOLD, 10);
        price = _Price;
    }
}
