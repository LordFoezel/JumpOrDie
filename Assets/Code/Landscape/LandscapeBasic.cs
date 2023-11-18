using UnityEngine;

public class LandscapeBasic : MonoBehaviour
{
    public enum AnimType { One, Two };
    public AnimType actualAnimType = AnimType.One;

    Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        if (actualAnimType == AnimType.One)
        {
            animator.SetBool("idle0", true);
            animator.SetBool("idle1", false);
        }
        else if (actualAnimType == AnimType.Two)
        {
            animator.SetBool("idle1", true);
            animator.SetBool("idle0", false);
        }
        else
        {
            animator.SetBool("idle0", true);
            animator.SetBool("idle1", false);
        }
    }

}
