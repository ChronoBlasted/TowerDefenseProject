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

    public void UpdateAmountGold(int newAmountOfGold)
    {
        goldAmountTxt.text = newAmountOfGold.ToString();
    }

    public void UpdateCurrentWave(int newAmountOfWave)
    {
        currentWaveTxt.text = newAmountOfWave.ToString();
    }

    public void UpdateTowerButtonState()
    {
        Dictionary<string, int> TowerBow = new Dictionary<string, int> // C'est bien overkill ca ahahah
        {
            { "Gold", 10 }
        };
        if (ResourceManager.Instance.EnoughRessource(TowerBow)) bowButton.interactable = true;
        else bowButton.interactable = false;

        Dictionary<string, int> TowerRock = new Dictionary<string, int>
        {
            { "Gold", 30 }
        };
        if (ResourceManager.Instance.EnoughRessource(TowerRock)) bowButton.interactable = true;
        else bowButton.interactable = false;
    }
}
