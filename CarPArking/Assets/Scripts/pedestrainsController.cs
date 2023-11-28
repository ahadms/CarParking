using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pedestrainsController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private Transform[] pathpoint;

    public float minDistance = 10;
    public int index = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        pathpoint = new Transform[PATH.transform.childCount];
        for (int i = 0; i < pathpoint.Length; i++)
        {
            pathpoint[i] = PATH.transform.GetChild(i);
        }
    }

    private void Update()
    {
        npcMove();
    }

   void npcMove()
    {
        if(Vector3.Distance(transform.position, pathpoint[index].position) < minDistance)
        {
            if(index >= 0 && index < pathpoint.Length)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
        }

        agent.SetDestination(pathpoint[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }
}
