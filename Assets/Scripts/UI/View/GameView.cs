using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    [SerializeField] TMP_Text goldAmountTxt;
    [SerializeField] TMP_Text currentWaveTxt;
    [SerializeField] Button bowButton, rockButton;

    public override void OpenView(float duration = 0.2F)
    {
        base.OpenView(duration);

        UpdateTowerButtonState();
    }

    public void UpdateAmountGold(int newAmountOfGold)
    {
        goldAmountTxt.text = newAmountOfGold.ToString();

        UpdateTowerButtonState();
    }
    public void DoColorAmountGold(Color colorText)
    {
        goldAmountTxt.DOColor(colorText, .2f).OnComplete(() =>
        {
            goldAmountTxt.DOColor(Color.white, .2f);
        });
    }
    public void DoShakeAmount()
    {
        goldAmountTxt.transform.DOShakePosition(.2f, new Vector3(20, 0, 0),20);
    }

    public void UpdateCurrentWave(int newAmountOfWave)
    {
        currentWaveTxt.text = "Wave : " + newAmountOfWave;
    }


    public void UpdateTowerButtonState()
    {
        Dictionary<RESOURCETYPE, int> TowerBow = new Dictionary<RESOURCETYPE, int> // C'est bien overkill ca ahahah
        {
            { RESOURCETYPE.GOLD, 10 }
        };
        if (ResourceManager.Instance.EnoughRessource(TowerBow)) bowButton.interactable = true;
        else bowButton.interactable = false;

        Dictionary<RESOURCETYPE, int> TowerRock = new Dictionary<RESOURCETYPE, int>
        {
            { RESOURCETYPE.GOLD, 30 }
        };
        if (ResourceManager.Instance.EnoughRessource(TowerRock)) rockButton.interactable = true;
        else rockButton.interactable = false;
    }

    public void HandleOnTowerButtonClick(GameObject selectedTower)
    {
        GridManager.Instance.ChooseTower(selectedTower.GetComponent<Tower>());
    }
}
