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


    void Start()
    {
        spawnNewPointGiverNow = SpawnNow();// assigning value to the variable
        spawnTimeCounter = Random.Range(pointGiverManager.GetMaximumTimeToSpawn(), pointGiverManager.GetMaximumTimeToSpawn());
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
        else
        {
            StopCoroutine(spawnNewPointGiverNow);
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
