using UnityEngine;

public class RefuelPoint : MonoBehaviour
{
    public bool isActive = true;
    public float cooldown = 10f;
    
    public Animator animator;
    public Collider2D hitbox;
    public LineRenderer line;


    void OnTriggerEnter2D(Collider2D other)
    {
        //disables refuel point after player collision
        if (other.tag == "Player" && isActive)
        {
            isActive = false;
            animator.SetBool("IsActive", false);
            AudioManager.instance.PlaySFX("Refuel", true);
            hitbox.enabled = false;
            line.enabled = false;

            //reactivates the refuel point after cooldown
            Invoke(nameof(Reactivate), cooldown);
        }
    }


    void Reactivate()
    {
        isActive = true;
        animator.SetBool("IsActive", true);
        hitbox.enabled = true;
    }
}
