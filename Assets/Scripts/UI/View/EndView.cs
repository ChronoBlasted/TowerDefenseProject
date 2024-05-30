using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndView : View
{
    [SerializeField] TMP_Text endWave;
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

    public void UpdateCurrentWave(int newAmountOfWave)
    {
        endWave.text = "Wave : " + newAmountOfWave;
    }
}
