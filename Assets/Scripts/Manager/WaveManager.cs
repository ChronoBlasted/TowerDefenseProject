using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyTypes;

    [Range(0, 100)]
    public int[] probability;
    private Dictionary<GameObject, int> monsters;

    [Space(10)]
    public int amountOfMonsterPerWave;
    public int currentWave;
    public int timeBetweenWaves;

    [Space(10)]
    public Transform[] spawnerList;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            monsters.Add(enemyTypes[i], probability[i]);
        }
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
        for (int i = 0; i < amountOfMonsterPerWave; i++)
        {
            ChooseRandomMonster();
        }
    }
}
