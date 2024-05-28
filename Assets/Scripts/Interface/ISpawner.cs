using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    GameObject ObjectToSpawn { get; set; }
    Vector3 SpawnPosition { get; set; }
    public void Spawn();
}
