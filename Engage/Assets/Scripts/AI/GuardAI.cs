using UnityEngine;
using UnityEngine.AI;
public class GuardAI : Pathfinding
{
    [SerializeField] protected Transform[] patrolPoints; //EmtpyObjects placed on the map at desired waypoint locations
    [SerializeField] protected float sightRange = 10f;
    [SerializeField] protected float viewAngle = 60f;
    [SerializeField] protected float timeToSearch = 5f; //Time to spend looking around after losing sight of the player
    [SerializeField] protected float stopChaseDistance = 2f; //The distance to the player at which the guard stops chasing
    [SerializeField] protected float retrySearchTime = 2f; //How much time until the guard loses track of the player after not being able to see him

    [SerializeField] protected float dangerZone = 1.5f; //The distance (by pathfinding, not through walls) at which the guard can hear/sense the player
    private NavMeshPath tempPath; //The temporary path for the dangerZone check in the CanSeePlayer method

    private float rotateSearchSpeed; //How fast the guard turns in place while searching

    private int currentPatrolIndex; //Index of the current point where the guard is in the patrolPoints array
    private Vector3 lastKnownPlayerPos;
    private float lostSightTimer; //How much time has passed since the guard last saw the player
    private float searchTime; //How much time has passed since the beginning of the search

    private enum State { Patrolling, Chasing, Searching } // The three states in which the guard can be
    private State currentState = State.Patrolling;

    protected override void Start()
    {
        base.Start();
        tempPath = new NavMeshPath();
        rotateSearchSpeed = 360 / timeToSearch; //setting the value of rotateSearchSpeed depending on timeToSearch
        GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                LookForPlayer();
                break;
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Searching:
                SearchForPlayer();
                break;
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        MoveTo(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void LookForPlayer()
    {
        if(CanSeePlayer()) currentState = State.Chasing;
    }
    
    void ChasePlayer()
    {
        if (player == null) return;

        Vector3 dirToPlayer = player.position - transform.position;
        float distance = dirToPlayer.magnitude;

        lastKnownPlayerPos = player.position;

        if (distance > stopChaseDistance) //If the guard hasn't yet reached the player, keep chasing
        {
            MoveTo(player.position);
        }
        else //If the guard has reached the player, change state to searching, but this can be modified to any other desired behaviour
        {
            agent.ResetPath();
            currentState = State.Searching;
        }

        if (distance > sightRange || !CanSeePlayer()) //If the guard lost sight of the player, go to the last location he was seen 
        {
            lostSightTimer += Time.deltaTime;
            if (lostSightTimer >= retrySearchTime)
            {
                currentState = State.Searching;
                MoveTo(lastKnownPlayerPos);
                searchTime = 0f;
                lostSightTimer = 0f;
            }
        }
        else
        {
            lostSightTimer = 0f;
        }
    }

    void SearchForPlayer()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            searchTime += Time.deltaTime;
            transform.Rotate(Vector3.up, rotateSearchSpeed * Time.deltaTime);

            if (searchTime >= timeToSearch)
            {
                currentState = State.Patrolling;
                GoToNextPatrolPoint();
            }
        }

        if (CanSeePlayer())
        {
            currentState = State.Chasing;
            lostSightTimer = 0f;
        }
    }

    bool CanSeePlayer() // returns true if the player is in the visible area (circular sector) of the guard, or if the shortest path to the player is less than dangerZone
    {
        if (player == null) return false;

        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
        Vector3 dirToPlayer = player.position - rayOrigin;
        float distanceToPlayer = dirToPlayer.magnitude;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        // Check if walkable path to player is shorter than dangerZone
        if (NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, tempPath))
        {
            float pathLength = 0f;
            if (tempPath.status == NavMeshPathStatus.PathComplete)
            {
                for (int i = 1; i < tempPath.corners.Length; i++)
                {
                    pathLength += Vector3.Distance(tempPath.corners[i - 1], tempPath.corners[i]);
                }

                if (pathLength <= dangerZone)
                {
                    //Debug.DrawLine(transform.position, player.position, Color.magenta); // Danger zone detection
                    return true;
                }
            }
        }

        // Normal sight logic
        if (distanceToPlayer <= sightRange && angle <= viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, dirToPlayer.normalized, out hit, sightRange))
            {
                //Debug.DrawLine(rayOrigin, hit.point, Color.green); // Sight ray

                if (hit.transform == player)
                {
                    return true;
                }
            }

            //Debug.DrawRay(rayOrigin, dirToPlayer.normalized * sightRange, Color.red); // Missed ray
        }

        return false;
    }




    void OnDrawGizmosSelected() //Gizmos :)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Vector3 leftRay = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightRay = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRay * sightRange);
        Gizmos.DrawRay(transform.position, rightRay * sightRange);
    }
}