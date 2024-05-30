using BaseTemplate.Behaviours;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RESOURCETYPE
{
    GOLD,
}

public class ResourceManager : MonoSingleton<ResourceManager>
{
    Dictionary<RESOURCETYPE, int> AmountOfResources = new Dictionary<RESOURCETYPE, int>();
    [SerializeField] int goldAmount; 

    void Start()
    {
        AmountOfResources.Add(RESOURCETYPE.GOLD, 100);
        goldAmount = AmountOfResources[RESOURCETYPE.GOLD];

       UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
    }


    public void AddResources(Dictionary<RESOURCETYPE, int> AmountToAdd)
    {
        foreach (RESOURCETYPE ressource in AmountToAdd.Keys)
        {
            AmountOfResources[ressource] += AmountToAdd[ressource];
        }
        goldAmount = AmountOfResources[RESOURCETYPE.GOLD];

        UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
    }

    public void SpendResources(Dictionary<RESOURCETYPE, int> AmountSpend)
    {
        foreach (RESOURCETYPE ressource in AmountSpend.Keys)
        {
            AmountOfResources[ressource] -= AmountSpend[ressource];
        }
        goldAmount = AmountOfResources[RESOURCETYPE.GOLD];

        UIManager.Instance.GameView.UpdateAmountGold(goldAmount);
        UIManager.Instance.GameView.DoColorAmountGold(Color.red);
    }

    public bool EnoughRessource(Dictionary<RESOURCETYPE, int> AmountNeeded)
    {   
        foreach(RESOURCETYPE ressource in AmountNeeded.Keys)
        {
            if (AmountNeeded[ressource] <= AmountOfResources[ressource])
            {
                return true;
            }
        }
        return false;
    }
}
