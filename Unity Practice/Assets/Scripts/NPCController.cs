using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggreRange = 10f;
    public Transform[] waypoints;

    private int index;
    private float speed, agentSpeed;
    private Transform player;

    private Animator arin;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake()
    {
        //arin=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agentSpeed = agent.speed;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);
        if(waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }
    void Patrol()
    {
        index = index == waypoints.Length-1 ? 0 : index+1;
    }
    void Tick()
    {
        agent.destination = waypoints[index].position;

        if (player != null && Vector3.Distance(transform.position, player.position)< aggreRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed * 2;
        }
    }

}
