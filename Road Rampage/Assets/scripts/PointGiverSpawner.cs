using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointGiverSpawner : MonoBehaviour
{
    [SerializeField] PointGiverManager pointGiverManager;
    [SerializeField] float spawnTimeCounter;
    [SerializeField] int spawnCounter;

    [SerializeField] float minimumX = -3f;
    [SerializeField] float maximumX = 3f;

    int direction = 1;
    float pointGiverAcceleration = 2f;

    [SerializeField] WaveManager waveManager;
    [SerializeField] WaveManager waveManager3;

    private string currentLevel;
    private LevelManager levelManager;

    void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        levelManager = FindFirstObjectByType<LevelManager>();

        // Always load the saved spawn count
        spawnCounter = GameState.SpawnCounter;

        // RoadRampage startup rules
        if (currentLevel == "RoadRampage")
        {
            ResetSpawnerState();       // includes health reset
            DisableObstaclesShooting();
        }

        // Level1 startup rules
        if (currentLevel == "Level1")
        {
            ResetSpawnerState_NoHealthReset();  // NEW
            EnableObstaclesShooting();
        }
    }

    private void ResetSpawnerState_NoHealthReset()
    {
        GameState.SpawnCounter = 0;
        spawnCounter = 0;
        // DO NOT reset GameState.PlayerHealth here
    }



    void Start()
    {
        spawnTimeCounter = Random.Range(
            pointGiverManager.GetMinimumTimeToSpawn(),
            pointGiverManager.GetMaximumTimeToSpawn() + 1f
        );
    }

    //Clean reset logic
    private void ResetSpawnerState()
    {
        GameState.SpawnCounter = 0;
        spawnCounter = 0;
        GameState.PlayerHealth = 100;
    }

    //Enable/Disable obstacle shooting
    public void EnableObstaclesShooting()
    {
        if (waveManager != null) waveManager.EnableObstaclesShooting();
        if (waveManager3 != null) waveManager3.EnableObstaclesShooting();
    }

    public void DisableObstaclesShooting()
    {
        if (waveManager != null) waveManager.DisableObstaclesShooting();
        if (waveManager3 != null) waveManager3.DisableObstaclesShooting();
    }

    //Movement logic
    void MovePointGiverContainer()
    {
        Vector2 pos = transform.position;
        pos.x += direction * pointGiverAcceleration * Time.deltaTime;

        if (pos.x >= maximumX)
        {
            pos.x = maximumX;
            direction = -1;
        }
        else if (pos.x <= minimumX)
        {
            pos.x = minimumX;
            direction = 1;
        }

        transform.position = pos;
    }

    //  Main spawn logic
    void SpawnPointGiver()
    {
        // GameOver check
        if (spawnCounter >= 20)
        {
            if (levelManager != null)
            {
                GameState.SpawnCounter = 0;
                GameState.PlayerHealth = 100;
                DisableObstaclesShooting();
                levelManager.LoadGameOver();
            }
            return;
        }

        //  Spawn new point givers
        spawnTimeCounter -= Time.deltaTime;
        if (spawnTimeCounter <= 0 && spawnCounter < 20)
        {
            StartCoroutine(SpawnNow());
            spawnCounter++;
            GameState.SpawnCounter = spawnCounter;

            spawnTimeCounter = Random.Range(
                pointGiverManager.GetMinimumTimeToSpawn(),
                pointGiverManager.GetMaximumTimeToSpawn() + 1f
            );
        }

        // Transition from RoadRampage  Level1
        if (spawnCounter == 10 && currentLevel == "RoadRampage")
        {
            if (levelManager != null)
            {
                EnableObstaclesShooting();
                levelManager.LoadSceneWithDelay("Level1");
            }
        }
    }

    //  Safe coroutine (no reuse)
    IEnumerator SpawnNow()
    {
        GameObject newPointGiver = Instantiate(
            pointGiverManager.GetPointGiverPrefab(),
            transform.position,
            Quaternion.identity
        );

        newPointGiver.GetComponent<Rigidbody2D>().linearVelocityY = -1f;

        yield return null;
    }

    void Update()
    {
        MovePointGiverContainer();
        SpawnPointGiver();
    }

    // Stop coroutine leaks
    void OnDestroy()
    {
        GameState.SpawnCounter = spawnCounter;
        StopAllCoroutines();
    }
}