using UnityEngine;

public class RefuelPoint : MonoBehaviour
{
    public bool isActive = true;
    public float time = 10f;
    public Animator animator;
    public Collider2D hitbox;
    public LineRenderer line;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            isActive = false;
            animator.SetBool("IsActive", false);
            AudioManager.instance.PlaySFX("Refuel", true);
            hitbox.enabled = false;
            line.enabled = false;
            Invoke(nameof(Reactivate), time);
        }
    }


    void Reactivate()
    {
        isActive = true;
        animator.SetBool("IsActive", true);
        hitbox.enabled = true;
    }
}
