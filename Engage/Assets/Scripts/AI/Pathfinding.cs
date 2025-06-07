using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] protected Transform player; //reference to the object we are finding a path to, usually the player
    protected NavMeshAgent agent;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected void MoveTo(Vector3 target)
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            if (agent.destination != target)
                agent.SetDestination(target);
            else if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathInvalid)
                agent.SetDestination(target); // force retry if stuck
        }
    }

    void Update()
    {
        if (player != null)
        {
            MoveTo(player.position);
        }
    }
}
