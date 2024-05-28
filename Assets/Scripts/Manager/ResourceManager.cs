using BaseTemplate.Behaviours;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    Dictionary<string, int> AmountOfResources = new Dictionary<string, int>();
    [SerializeField] int goldAmount; 
    // Start is called before the first frame update
    void Start()
    {
        AmountOfResources.Add("Gold", 10);
        goldAmount = AmountOfResources["Gold"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendResources(Dictionary<string, int> AmountSpend)
    {
        foreach (string ressource in AmountSpend.Keys)
        {
            AmountOfResources[ressource] -= AmountSpend[ressource];
        }
        goldAmount = AmountOfResources["Gold"];
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
    public void AddResources(int n)
    {
        
    }
}
