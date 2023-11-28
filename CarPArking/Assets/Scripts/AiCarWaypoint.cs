using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCarWaypoint : MonoBehaviour
{
    [Header("Opponent Car")]
    public AiCarController aiCar;
    public WayPoint currentWaypoint;
    public WayPoint initialWaypoint; // Store the initial waypoint here

    private void Awake()
    {
        aiCar = GetComponent<AiCarController>();
    }

    private void Start()
    {
        aiCar.LocateDestination(currentWaypoint.GetPosition());
        initialWaypoint = currentWaypoint; // Set the initial waypoint
    }

    private void Update()
    {
        if (aiCar.destinationReached)
        {
            if (currentWaypoint.nextWaypoint != null)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else
            {
                // If the current waypoint is the last one, set it back to the initial waypoint
                currentWaypoint = initialWaypoint;
            }

            aiCar.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
