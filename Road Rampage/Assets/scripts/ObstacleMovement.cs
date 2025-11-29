using UnityEngine;
using System.Collections.Generic;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] List<Transform> obstcleWaypointList;
    [SerializeField] float obstacleAcceleration = 3f;
    int obstacleWaypointIndex = 0;
    [SerializeField] WaveManager waveManager;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the obstacle on the first path of the waypoint
        obstcleWaypointList = waveManager.GetpathPrefab();
        //set the current obstacle position on the first waypoint
        var currentObstaclePosition = obstcleWaypointList[obstacleWaypointIndex].transform.position;
    }

    //create a method to define obstacle movement along the waypoint
    void ObstacleMovementNow() 
    {
        //check that the obstacle movement is not beyond that waypointlist
        if (obstacleWaypointIndex < obstcleWaypointList.Count)
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
            //check to see if the obstacle has arrived on the waypoint
            if (transform.position == targetPosition)
            {
                //if the obstacle arrive on a waypoint then move to the next waypoint in the obstcleWaypointList
                obstacleWaypointIndex++;
            }

        }
        else 
        {
            //
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //start the obstacle movement on waypoint
        ObstacleMovementNow();
    }
}
