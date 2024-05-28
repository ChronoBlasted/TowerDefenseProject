using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public GameObject[] enemyTypes;

    [Range(0, 100)]
    public int[] probability;
    private Dictionary<GameObject, int> monsters;

    [Space(10)]
    public int amountOfMonsterPerWave;
    public int amountOfMonsterAddedPerWave;
    public int currentWave;
    public int timeBetweenWaves;

    [Space(10)]
    public Transform[] spawnerList;

    // Start is called before the first frame update
    void Start()
    {
        monsters = new Dictionary<GameObject, int>();
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            monsters.Add(enemyTypes[i], probability[i]);
        }
        StartCoroutine(NextWaveCoroutine());
    }

    public void ChooseRandomMonster()
    {
        int totalProbability = 0;
        foreach (int probability in monsters.Values)
        {
            totalProbability += probability;
        }

        int randomValue = Random.Range(0, totalProbability);

        foreach (KeyValuePair<GameObject, int> entry in monsters)
        {
            if (randomValue < entry.Value)
            {
                Instantiate(entry.Key, spawnerList[Random.Range(0, spawnerList.Length)]);
                break;
            }
            else
            {
                randomValue -= entry.Value;
            }
        }
    }

    public void CheckNextWave()
    {
        AMonster[] remainingMonsterList = FindObjectsOfType<AMonster>();
        if (remainingMonsterList.Length == 0 ) 
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }

    private IEnumerator NextWaveCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        for (int i = 0; i < (amountOfMonsterPerWave + (currentWave * amountOfMonsterAddedPerWave)) ; i++)
        {
            ChooseRandomMonster();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
