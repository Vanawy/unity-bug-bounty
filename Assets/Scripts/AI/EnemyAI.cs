using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
using System.Threading.Tasks;

[HelpURL("http://arongranberg.com/astar/docs/class_partial1_1_1_astar_a_i.php")]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {
    public Transform targetPosition;

    private Seeker _seeker;
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _sr;

    public Path path;

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float nextWaypointDistance = 1;
    private float pathUpdateTime = 1f;

    private int _currentWaypoint = 0;
    private bool reachedEndOfPath;
    

    public void Awake () {
        _seeker = GetComponent<Seeker>();
        _animator = GetComponent<Animator>();
    }

    public async void OnPathComplete (Path p) {

        if (p.error) {
            Debug.Log("A path calculation error: " + p.errorLog);
            return;
        }
        path = p;
        // Reset the waypoint counter so that we start to move towards the first point in the path
        _currentWaypoint = 0;
        await Task.Delay((int) (pathUpdateTime * 1000));
        _seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void FixedUpdate () {
        if (path == null) {
            // We have no path to follow yet, so don't do anything
            if (targetPosition == null) {
                return;
            }
            _seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true) {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[_currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // Check if there is another waypoint or if we have reached the end of the path
                if (_currentWaypoint + 1 < path.vectorPath.Count) {
                    _currentWaypoint++;
                } else {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[_currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;
        bool isMoving = false;
        if (velocity.x > 0) {
            isMoving = true;
            _sr.flipX = false;
        } else if (velocity.x < 0) {
            isMoving = true;
            _sr.flipX = true;
        }
        _animator.SetBool("is_moving", isMoving);

        transform.position += velocity * Time.deltaTime;
    }
}