using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrainSpanner : MonoBehaviour
{
    public GameObject pedestrainPrefab;
    public int pedestrainSpawn;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minDistanceBetweenPedestrians = 3f;

    private List<Vector3> instantiatedPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;

        while (count < pedestrainSpawn)
        {
            Vector3 randomPosition = GetRandomPosition();

            // Check if the random position is far enough from existing positions
            bool positionIsValid = true;
            foreach (Vector3 pos in instantiatedPositions)
            {
                float distance = Vector3.Distance(randomPosition, pos);
                if (distance < minDistanceBetweenPedestrians)
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid)
            {
                GameObject obj = Instantiate(pedestrainPrefab);
                instantiatedPositions.Add(randomPosition);
                obj.transform.position = randomPosition;

                Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
                obj.GetComponent<WayPointNavigator>().currentWaypoint = child.GetComponent<WayPoint>();

                NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
                agent.speed = Random.Range(minSpeed, maxSpeed); // Set random speed

                count++;
            }

            yield return null;
        }
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random position within the area where pedestrians can spawn
        float x = Random.Range(transform.position.x - 10f, transform.position.x + 10f);
        float z = Random.Range(transform.position.z - 10f, transform.position.z + 10f);
        return new Vector3(x, 0, z);
    }
}
