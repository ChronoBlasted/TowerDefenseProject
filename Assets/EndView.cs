using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : View
{
    public void ReloadGame()
    {
        GameManager.Instance.ReloadScene();
    }
}
