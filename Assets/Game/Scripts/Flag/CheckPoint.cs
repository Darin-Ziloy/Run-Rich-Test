using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator animator;

    public void SetCheckPoint()
    {
        animator.Play("SetCheckPoint");
        enabled = false;
    }
}