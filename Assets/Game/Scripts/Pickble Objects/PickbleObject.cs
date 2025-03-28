using UnityEngine;

public class PickbleObject : MonoBehaviour, IPickbleObject
{
    [SerializeField] private PickbleObjectType type;
    [SerializeField] private float rich = 5f;
    [SerializeField] private int money = 10;

    [Header("Rotation Settings")]
    public bool rotate = true;
    public float rotationSpeed = 1f;
    public Vector3 rotationAxis = Vector3.up;
    
    [Header("Wave Settings")]
    public bool wave = false;
    public float waveSpeed = 1f;
    public float waveHeight = 1f;
    public Vector3 waveAxis = Vector3.up;

    public GameObject model;
    public ParticleSystem pickUpEffect;
    public Transform pickUpEffectPosition;

    public PickbleObjectType Type => type;
    public float Rich => rich;
    public int Money => money;

    void Update()
    {
        Rotate();
        UpDown();
    }

    void Rotate()
    {
        if (!rotate) return;
        model.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    void UpDown()
    {
        if (!wave) return;
        float yOffset = (Mathf.Sin(Time.time * waveSpeed) + 1f) * 0.5f * waveHeight;
        model.transform.position = transform.position + waveAxis * yOffset;
    }
    
    public void PickUp()
    {
        if (pickUpEffect != null)
        {
            Instantiate(pickUpEffect, pickUpEffectPosition.position, pickUpEffectPosition.rotation);
        }
        
        Destroy(gameObject);
    }
}

public interface IPickbleObject
{
    PickbleObjectType Type { get; }
    float Rich { get; }
    int Money { get; }
    void PickUp();
}

public enum PickbleObjectType
{
    None,
    Money,
    Bad
}