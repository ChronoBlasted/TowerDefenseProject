using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartView : View
{
    public void HandleOnPlayClick()
    {
        GameManager.Instance.UpdateStateToGame();
    }
}
