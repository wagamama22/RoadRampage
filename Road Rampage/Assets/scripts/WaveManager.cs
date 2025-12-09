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
    [SerializeField] bool obstaclesCanShoot = false;


    public bool GetObstaclesCanShoot()
    {
        return obstaclesCanShoot;
    }
    // Called by PointGiverSpawner when threshold reached
    public void EnableObstaclesShooting()
    {
        obstaclesCanShoot = true;
        Debug.Log("WaveManager: obstacles can now shoot!");
    }

    public void DisableObstaclesShooting()
    {
        obstaclesCanShoot = false;
        Debug.Log("WaveManager: obstacles cannot shoot now");
    }

    //encapsulate all
    public GameObject GetobstaclePrefab
    {
        get { return obstaclePrefab; }
        set { obstaclePrefab = value; }
    }
    public List<Transform> GetPathPrefab
    {
        get 
        {
            List<Transform> wavePathList = new List<Transform>();
            foreach (Transform waypoint in pathPrefab.transform)
            {
                wavePathList.Add(waypoint);
            }
            return wavePathList;
        }
    }
    public int GetnumberOfObstacleToSpawn
    {
        get { return numberOfObstacleToSpawn; }
        set { numberOfObstacleToSpawn = value; }
       
    }
    public float GetdelaySpawnTime 
    {
        get { return delaySpawnTime; }
        set { delaySpawnTime = value; }
        
    }
    public float GetobstacleMovementSpeed 
    {
        get { return obstacleMovementSpeed; }
        set { obstacleMovementSpeed = value; }
        
    }
}
