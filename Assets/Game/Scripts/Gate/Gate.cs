using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isGoodGate = true;
    public float richToAdd = 10;
    public int moneyToAdd = 10;

    public ParticleSystem pickUpEffect;
    public Transform pickUpEffectPosition;

    public Gate otherGate;
    public Collider gateCollider;

    public void EnterGate()
    {
        gateCollider.enabled = false;
        
        if (pickUpEffect != null)
        {
            Instantiate(pickUpEffect, pickUpEffectPosition.position, pickUpEffectPosition.rotation);
        }

        if (otherGate != null)
        {
            otherGate.gateCollider.enabled = false;
        }
    }
}