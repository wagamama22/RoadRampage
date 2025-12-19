using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointGiverSpawner : MonoBehaviour
{
    [SerializeField] PointGiverManager pointGiverManager;
    [SerializeField] float spawnTimeCounter;
    [SerializeField] int spawnCounter;
    IEnumerator spawnNewPointGiverNow;//declaring the variable
    [SerializeField] float minimumX = -3f;
    [SerializeField] float maximumX = 3f;
    int direction = 1; // 1 = moving right, -1 = moving left
    float pointGiverAcceleration = 2f;
    [SerializeField] WaveManager waveManager;
    [SerializeField] WaveManager waveManager3;

    private string currentLevel;
    private LevelManager levelManager;

    void Awake()
    {
        spawnCounter = GameState.SpawnCounter;
        currentLevel = SceneManager.GetActiveScene().name;
        levelManager = FindFirstObjectByType<LevelManager>();

        if (currentLevel == "RoadRampage")
        {
            if (GameState.SpawnCounter > 20 || GameState.SpawnCounter == 0)
            {
                GameState.SpawnCounter = 0;
                spawnCounter = 0;
                GameState.PlayerHealth = 100;
                DisableObstaclesShooting();
            }
        }
    }

    void Start()
    {
        spawnNewPointGiverNow = SpawnNow();// assigning value to the variable
        spawnTimeCounter = Random.Range(pointGiverManager.GetMaximumTimeToSpawn(), pointGiverManager.GetMaximumTimeToSpawn() + 1f);
    }

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


    void MovePointGiverContainer()
    {
        // Current position
        Vector2 currentPosition = transform.position;

        // Move along X axis
        currentPosition.x += direction * pointGiverAcceleration * Time.deltaTime;

        // Check boundaries
        if (currentPosition.x >= maximumX)
        {
            currentPosition.x = maximumX;
            direction = -1; // reverse direction
        }
        else if (currentPosition.x <= minimumX)
        {
            currentPosition.x = minimumX;
            direction = 1; // reverse direction
        }

        // Apply movement
        transform.position = currentPosition;

    }

    void SpawnPointGiver()
    {
        // CRITICAL FIX: Check GameOver FIRST
        if (spawnCounter >= 20)
        {
            if (levelManager != null)
            {
                GameState.SpawnCounter = 0;
                GameState.PlayerHealth = 100;
                DisableObstaclesShooting();
                levelManager.LoadSceneByName("GameOver");
            }
            return;
        }

        // Spawn logic SECOND
        spawnTimeCounter -= Time.deltaTime;
        if (spawnTimeCounter <= 0 && spawnCounter < 20)
        {
            StartCoroutine(spawnNewPointGiverNow);
            spawnCounter++;
            GameState.SpawnCounter = spawnCounter;

            spawnTimeCounter = Random.Range(
                pointGiverManager.GetMinimumTimeToSpawn(),
                pointGiverManager.GetMaximumTimeToSpawn() + 1f
            );
        }

        // Level transition
        if (spawnCounter == 10 && currentLevel == "RoadRampage")
        {
            if (levelManager != null)
            {
                EnableObstaclesShooting();
                levelManager.LoadSceneWithDelay("Level1");
            }
        }
    }

    IEnumerator SpawnNow()
    {
        while (true)
        {
            GameObject newPointGiver = Instantiate(pointGiverManager.GetPointGiverPrefab(), transform.position, Quaternion.identity);
            newPointGiver.GetComponent<Rigidbody2D>().linearVelocityY = -1f;

            //applying coroutine delay
            yield return new WaitForSeconds(spawnTimeCounter);
        }

    }


    // Update is called once per frame
    void Update()
    {
        MovePointGiverContainer();
        SpawnPointGiver();

    }

    void OnDestroy()
    {
        GameState.SpawnCounter = spawnCounter;
    }

}
