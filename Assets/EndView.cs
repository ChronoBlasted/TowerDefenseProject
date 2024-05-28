using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : View
{
    public void ReloadGame()
    {
        StartCoroutine(endCoroutine());
    }

    IEnumerator endCoroutine()
    {
        UIManager.Instance.ShowBlackShade();

        yield return new WaitForSeconds(.5f);

        GameManager.Instance.ReloadScene();
    }
}
