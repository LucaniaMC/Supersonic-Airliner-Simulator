using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
   Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        float offset = Random.Range(0f,1f);
        animator.Play(0, 0, offset);
    }
}
