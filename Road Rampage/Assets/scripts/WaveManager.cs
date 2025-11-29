using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveManager", menuName = "Scriptable Objects/WaveManager")]
public class WaveManager : ScriptableObject
{
    [SerializeField] float obstacleMovementSpeed = 3f;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int numberOfObstacleToSpawn = 5;
    [SerializeField] float delaySpawnTime = 1.0f;

    //encapsulate all
    public GameObject GetobstaclePrefab() 
    {
        return obstaclePrefab;
    }
    public List<Transform> GetpathPrefab() 
    {
        List<Transform> wavePathList = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform) 
        {
            wavePathList.Add(waypoint);
        }
        return wavePathList;
    }
    public int GetnumberOfObstacleToSpawn() 
    {
        return numberOfObstacleToSpawn;
    }
    public float GetdelaySpawnTime() 
    {
        return delaySpawnTime;
    }
    public float GetobstacleMovementSpeed() 
    {
        return obstacleMovementSpeed;
    }
}
