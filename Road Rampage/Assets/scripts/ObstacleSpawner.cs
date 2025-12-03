using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<WaveManager> waveManagerList;
    [SerializeField] int waveManagerListIndex = 0;
    WaveManager waveManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoopWavesForever());
    }

    IEnumerator LoopWavesForever()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = waveManagerListIndex; waveIndex < waveManagerList.Count; waveIndex++)
        {
            //start from first wave
            waveManager = waveManagerList[waveIndex]; //  Assign waveManager here
            yield return StartCoroutine(SpawnObstacleInSingleWave());
        }
    }

    // Using coroutine to spawn one wave of enemies
    IEnumerator SpawnObstacleInSingleWave()
    {
        List<Transform> pathPrefabs = waveManager.GetPathPrefab;
        int totalPaths = pathPrefabs.Count;
        int obstacleCount = 1;

        while (obstacleCount <= waveManager.GetnumberOfObstacleToSpawn)
        {
            // Spawn object from the first/starting path
            Vector3 spawnPosition = pathPrefabs[0].transform.position;
            GameObject newObstacle = Instantiate(waveManager.GetobstaclePrefab, spawnPosition, Quaternion.identity);
            newObstacle.transform.position = spawnPosition; // Force position before first render
            obstacleCount++;

            // Delay before spawning the next obstacle
            yield return new WaitForSeconds(waveManager.GetdelaySpawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}