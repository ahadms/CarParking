using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointNavigator : MonoBehaviour
{
    public NavMeshAgent agent;

    public WayPoint currentWaypoint;

    private enum Direction
    {
        Forward,
        Backward
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(currentWaypoint.GetPosition());
    }

    private Direction direction = Direction.Forward;

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            bool shouldBranch = false;

            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio;
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
            }
            else
            {
                if (direction == Direction.Forward && currentWaypoint.nextWaypoint != null)
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                }
                else if (direction == Direction.Backward && currentWaypoint.previousWaypoint != null)
                {
                    currentWaypoint = currentWaypoint.previousWaypoint;
                }
                else
                {
                    // Change direction if there is no valid next or previous waypoint.
                    direction = (direction == Direction.Forward) ? Direction.Backward : Direction.Forward;
                }
            }

            agent.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
