using UnityEngine;

public class MoveByFinger : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float powerModifier = 100;
    [SerializeField] private float xOffset = 4f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private bool localMovement = true;

    private Vector3 newPosition = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero; // Added for smoothing

    void Start()
    {
        if (target == null)
        {
            target = transform;
            #if UNITY_EDITOR || DEVELOPMENT_BUILD || DEBUG
            Debug.LogError("No target set for MoveByFinger script. Using the object this script is attached to as the target.");
            #endif
        }
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float newX = mouseX * powerModifier * Time.deltaTime;

            newPosition = localMovement ? target.localPosition : target.position;
            newPosition.x += newX;
            newPosition.x = Mathf.Clamp(newPosition.x, -xOffset, xOffset);
        }

        Vector3 currentPos = localMovement ? target.localPosition : target.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(currentPos, newPosition, ref currentVelocity, 1f / smoothSpeed);

        if (localMovement)
        {
            target.localPosition = smoothedPosition;
        }
        else
        {
            target.position = smoothedPosition;
        }
    }
}
