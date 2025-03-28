using UnityEngine;
using PathCreation;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public bool findClosestPointAtStart = true;
    public bool rotationIgnoreXZ = true;
    public float speed = 5;
    public float distanceTravelled;
    void Start() {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            if (findClosestPointAtStart)
            {
                // Set the distance travelled to the closest point on the path to the follower's starting position
                distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            }
            else
            {
                // Set the distance travelled to the start of the path
                distanceTravelled = 0;
            }
        }
    }
    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            if(rotationIgnoreXZ)
            {
                // Rotate the follower to face the direction of the path
                Quaternion newRotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                newRotation.x = 0;
                newRotation.z = 0;
                transform.rotation = newRotation;
            }
            else
            {
                // Rotate the follower to face the direction of the path
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}