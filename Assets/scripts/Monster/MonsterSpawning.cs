using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSpawning : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string Name;
        public List<MonsterGroup> monsterGroups;
        public int totalWaveMonsterCount; // total monster count in the wave
        public float spawnInterval; // time between monster spawns
        public int currentWaveSpawnCount; // amount of monsters that have been spawns
    }

    [System.Serializable]
    public class MonsterGroup
    {
        public string monsterName;
        public int currentMonsterCount;
        public int totalMonsterSpawnCount;
        public GameObject monsterPrefab;
    }

    public List<Wave> waves;
    public int currentWave;
    public float spawnTimer;
    public float waveTimer;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        WaveCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave < waves.Count && waves[currentWave].currentWaveSpawnCount == 0)
        {
            StartCoroutine(NextWave());
        }
        spawnTimer += Time.deltaTime;

        if (spawnTimer > waves[currentWave].spawnInterval)
        {
            SpawnEnemies();
            spawnTimer = 0f;
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveTimer);

        if (currentWave < waves.Count - 1)
        {
            currentWave++;
            WaveCount();
        }
    }

    void WaveCount()
    {
        int monsterCount = 0;
        foreach (var monsterGroup in waves[currentWave].monsterGroups)
        {
            monsterCount += monsterGroup.totalMonsterSpawnCount;
        }
        waves[currentWave].totalWaveMonsterCount = monsterCount;
    }

    void SpawnEnemies()
    {
        // if spawned amount is less than total monster count of this wave
        if (waves[currentWave].currentWaveSpawnCount < waves[currentWave].totalWaveMonsterCount)
        {
            // for every monster group in the wave
            foreach (var monsterGroup in waves[currentWave].monsterGroups)
            {
                // if the specific monster spawn count is less than total monster of that type in the wave
                if (monsterGroup.currentMonsterCount < monsterGroup.totalMonsterSpawnCount)
                {
                    Vector2 spawnPos =
                        new(
                            player.transform.position.x
                                + Random.Range(5f, 10f) * (Random.Range(0, 2) == 0 ? -1 : 1),
                            player.transform.position.y
                                + Random.Range(5f, 10f) * (Random.Range(0, 2) == 0 ? -1 : 1)
                        );
                    Instantiate(monsterGroup.monsterPrefab, spawnPos, Quaternion.identity);

                    monsterGroup.currentMonsterCount++;
                    waves[currentWave].currentWaveSpawnCount++;
                }
            }
        }
    }
}
