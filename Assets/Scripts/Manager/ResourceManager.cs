using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    Dictionary<string, int> AmountOfResources = new Dictionary<string, int>();
    [SerializeField] int goldAmount; 

    void Start()
    {
        AmountOfResources.Add("Gold", 10);
        goldAmount = AmountOfResources["Gold"];

        UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
    }


    public void AddResources(Dictionary<string, int> AmountToAdd)
    {
        foreach (string ressource in AmountToAdd.Keys)
        {
            AmountOfResources[ressource] += AmountToAdd[ressource];
        }
        goldAmount = AmountOfResources["Gold"];

        UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
    }

    public void SpendResources(Dictionary<string, int> AmountSpend)
    {
        foreach (string ressource in AmountSpend.Keys)
        {
            AmountOfResources[ressource] -= AmountSpend[ressource];
        }
        goldAmount = AmountOfResources["Gold"];

        UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
    }

    public bool EnoughRessource(Dictionary<string, int> AmountNeeded )
    {
        foreach(string ressource in AmountNeeded.Keys)
        {
            if (AmountNeeded[ressource] <= AmountOfResources[ressource])
            {
                return true;
            }
        }
        return false;
    }
}
