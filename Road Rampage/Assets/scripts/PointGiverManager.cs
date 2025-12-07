using UnityEngine;

[CreateAssetMenu(fileName = "PointGiverManager", menuName = "Scriptable Objects/PointGiverManager")]
public class PointGiverManager : ScriptableObject
{
    [SerializeField] GameObject pointGiverPrefab;
    [SerializeField] float minimumTimeToSpawn = 0.01f;
    [SerializeField] float maximumTimeToSpawn = 10f;


    public GameObject GetPointGiverPrefab()
    {
        return pointGiverPrefab;
    }

    public float GetMinimumTimeToSpawn()
    {
        return minimumTimeToSpawn;
    }

    public float GetMaximumTimeToSpawn()
    {
        return maximumTimeToSpawn;
    }

}

