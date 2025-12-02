using UnityEngine;
using System.Collections.Generic;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] List<Transform> obstcleWaypointList;
    [SerializeField] float obstacleAcceleration = 3f;
    int obstacleWaypointIndex = 0;
    [SerializeField] WaveManager waveManager;
    DestroyAny destroyAny;

    void Awake()
    {
        // Initialize destroyAny component
        destroyAny = GetComponent<DestroyAny>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the obstacle on the first path of the waypoint
        obstcleWaypointList = waveManager.GetpathPrefab;
        //set the current obstacle position on the first waypoint
        var currentObstaclePosition = obstcleWaypointList[obstacleWaypointIndex].transform.position;
    }

    //create a method to define obstacle movement along the waypoint
    void ObstacleMovementNow() 
    {
        //check that the obstacle movement is not beyond that waypointlist
        bool hasWaypoints = obstacleWaypointIndex < obstcleWaypointList.Count;
        if (hasWaypoints)
        {
            //get the current position of the obstacle  on the first waypoint
            var currentObstaclePosition = obstcleWaypointList[obstacleWaypointIndex].transform.position;
            //set the current position of the obstacle  on the first waypoint to be the target position
            var targetPosition = currentObstaclePosition;
            //ensure the obstacle movement remain in 2d
            targetPosition.z = 0f;
            //set the obstacle speed in each frameset
            float obstacleMoveSpeedOnFrame = obstacleAcceleration * Time.deltaTime;
            //move obstacle from current position to targetposition per frameset
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, obstacleMoveSpeedOnFrame);
            // Advance to next waypoint if reached
            if (Vector2.Distance(transform.position, targetPosition) <= 0f)//check if obstacle has reached targetPosition
            {
                obstacleWaypointIndex++;
            }

        }
        else 
        {
            // Trigger destruction when path is complete
            destroyAny.GetDamageAttached();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //start the obstacle movement on waypoint
        ObstacleMovementNow();
    }
}
