using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerScript : MonoBehaviour
{
    public GameObject[] spawnEnemyTypes;
    //[Tooltip("The Spawning Weight For each element in the enemy types list")] public int[] spawnEnemyWeight;
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    public int baseSpawnEnemyCount = 4;
    public int waveCount = 0;
    public float waveRestDuration = 30f;
    private float waveRestTimer;
    private int dynamicSpawnEnemyCount = 4;
    private UIManager uiManager;
    private float timeTemp;
    private PlayerManager player;

    private bool flag = false;
    private bool playerKilled = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (player.stats.health <= 0)
        {
            if (!playerKilled) KillEnemyList();
            if (!playerKilled) playerKilled = true;
            return;
        }
        timeTemp += Time.deltaTime;
        if (enemyList.Count > 0) UpdateEnemyList();
        if (enemyList.Count == 0)
        {
            if (!flag)
            {
                InitializeNextWave();
                Invoke(nameof(SpawnEnemyList), waveRestDuration);
                flag = true;
            }
            if(timeTemp >= 1f)
            {
                waveRestTimer--;
                timeTemp = 0f;
            }
            uiManager.roundRestDurationTimer.text = waveRestTimer.ToString();
            if (!uiManager.roundRestDurationTimer.gameObject.activeSelf) uiManager.roundRestDurationTimer.gameObject.SetActive(true);
            if (uiManager.roundCount.gameObject.activeSelf) uiManager.roundCount.gameObject.SetActive(false);
        }
    }
    void UpdateEnemyList()
    {
        if (uiManager.roundRestDurationTimer.gameObject.activeSelf) uiManager.roundRestDurationTimer.gameObject.SetActive(false);
        if (!uiManager.roundCount.gameObject.activeSelf) uiManager.roundCount.gameObject.SetActive(true);
        for (int i = 0; i < enemyList.Count; i++)
        {
            float healthTemp = enemyList[i].gameObject.GetComponentInChildren<EnemyAI>().health;
            if (healthTemp <= 0f)
            {
                enemyList.Remove(enemyList[i]);
                if (enemyList.Count == 0 && flag)
                {
                    waveRestTimer = waveRestDuration;
                    timeTemp = 0f;
                    flag = false;
                }
            }
        }
    }
    void KillEnemyList()
    {
        while(enemyList.Count > 0)
        {
            Destroy(enemyList[0].gameObject);
            enemyList.Remove(enemyList[0]);
        }
        /*for (int i = 0; i < enemyList.Count; i++)
        {
            Destroy(enemyList[i].gameObject);
            enemyList.Remove(enemyList[i]);
        }*/
    }
    void SpawnEnemyList()
    {
        int tmp = dynamicSpawnEnemyCount;
        while (tmp != 0)
        {
            InstantiateEnemy();
            tmp--;
        }
    }
    void InitializeNextWave()
    {
        waveCount++;
        dynamicSpawnEnemyCount = 2 * waveCount;
        uiManager.roundCount.text = "Round " + waveCount.ToString();
    }
    void InstantiateEnemy()
    {
        GameObject tmp = Instantiate(spawnEnemyTypes[0], new Vector3(Random.Range(transform.position.x + 7, transform.position.x - 7), transform.position.y - 10, Random.Range(transform.position.z + 7, transform.position.z - 7)), transform.rotation);
        enemyList.Add(tmp);
    }
}
