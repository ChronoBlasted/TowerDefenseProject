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
    public int[] cantAppearUntilWave;

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
        GameManager.Instance.OnGameStateChanged += HandleStateChange;
        currentWave = 0;
        UIManager.Instance.GameView.UpdateCurrentWave(currentWave);
    }

    void HandleStateChange(GAMESTATE newState)
    {
        switch (newState)
        {
            case GAMESTATE.START:
                break;
            case GAMESTATE.GAME:

                monsters = new Dictionary<GameObject, int>();
                for (int i = 0; i < enemyTypes.Length; i++)
                {
                    monsters.Add(enemyTypes[i], probability[i]);
                }
                StartCoroutine(NextWaveCoroutine());

                break;
            case GAMESTATE.END:
                UIManager.Instance.EndView.UpdateCurrentWave(currentWave);
                //Faire Disparaitre les enemis
                AMonster[] remainingMonsterList = FindObjectsOfType<AMonster>();
                foreach (AMonster m in remainingMonsterList)
                {
                    Destroy(m.gameObject);
                }

                break;
            default:
                break;
        }
    }

    public void ChooseRandomMonster()
    {
        int totalProbability = 0;
        /*foreach (int probability in monsters.Values)
        {
            totalProbability += probability;
        }*/
        for (int i = 0; i < probability.Length; i++)
        {
            if (cantAppearUntilWave[i] <= currentWave)
            {
                totalProbability += probability[i];
            }
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
        StartCoroutine(CheckNextWaveCoroutine());
    }

    IEnumerator CheckNextWaveCoroutine()
    {
        yield return new WaitForEndOfFrame();

        AMonster[] remainingMonsterList = FindObjectsOfType<AMonster>();
        if (remainingMonsterList.Length == 0 && GameManager.Instance.gameState == GAMESTATE.GAME)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }

    private IEnumerator NextWaveCoroutine()
    {
        currentWave++;
        UIManager.Instance.GameView.UpdateCurrentWave(currentWave);

        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < (amountOfMonsterPerWave + (currentWave * amountOfMonsterAddedPerWave)) ; i++)
        {
            ChooseRandomMonster();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
