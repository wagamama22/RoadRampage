using System.Collections;
using UnityEngine;

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


    void Start()
    {
        spawnNewPointGiverNow = SpawnNow();// assigning value to the variable
        spawnTimeCounter = Random.Range(pointGiverManager.GetMaximumTimeToSpawn(), pointGiverManager.GetMaximumTimeToSpawn());
    }

    public void EnableObstaclesShooting()
    {
        if (waveManager != null && waveManager3 != null)
        {
            waveManager.EnableObstaclesShooting();
            waveManager3.EnableObstaclesShooting();
        }
    }

    public void DisableObstaclesShooting()
    {
        if (waveManager != null && waveManager3 != null)
        {
            waveManager.DisableObstaclesShooting();
            waveManager3.DisableObstaclesShooting();
        }
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
        //count down the spawntimecou nter  each frame
        spawnTimeCounter -= Time.deltaTime;

        if (spawnTimeCounter <= 0 && spawnCounter < 10)
        {
            StartCoroutine(spawnNewPointGiverNow);
            spawnCounter++;
            //reset the counter
            spawnTimeCounter = Random.Range(pointGiverManager.GetMaximumTimeToSpawn(), pointGiverManager.GetMaximumTimeToSpawn());
        }
        else if (spawnCounter >= 10)
        {
            LevelManager levelManager = FindFirstObjectByType<LevelManager>();
            // Notify WaveManager
            if (waveManager != null && waveManager3 != null && levelManager != null)
            {
                levelManager.LoadSceneWithDelay("Level1");
                waveManager.EnableObstaclesShooting();
                waveManager3.EnableObstaclesShooting();
            }

        }
        else if (spawnCounter > 10 && spawnCounter < 20)
        {
            LevelManager levelManager = FindFirstObjectByType<LevelManager>();
            // Notify WaveManager
            if (waveManager != null && waveManager3 != null && levelManager != null)
            {
                waveManager.DisableObstaclesShooting();
                waveManager3.DisableObstaclesShooting();
                levelManager.LoadSceneByName("RoadRampage");

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
}
