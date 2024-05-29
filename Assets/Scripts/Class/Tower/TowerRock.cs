using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRock : Tower
{
    public Dictionary<RESOURCETYPE, int> _Price = new Dictionary<RESOURCETYPE, int>();
    void Start()
    {
        _Price.Add(RESOURCETYPE.GOLD, 30);
        price = _Price;
    }

}
