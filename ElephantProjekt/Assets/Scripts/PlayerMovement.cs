using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    public Animator anim;

    public float jumpForce = 15f;
    public Transform feet;
    public LayerMask groundLayers;

    [HideInInspector] public bool isFacingRight = true;

    float mx;
    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    private void Update()
    {
        mx = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Vertical") && IsGrounded())
        {
            Jump();
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Mathf.Abs(mx) > 0.05f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (mx > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isFacingRight = true;
        }
        else if (mx < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isFacingRight = false;
        }
        anim.SetBool("isGrounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }
    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }

        return false;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animationSet("idle0");
        }
    }

    private void animationReset()
    {
        if (!stateInfo.IsName("Base Layer.idle0"))
        {
            animator.SetBool("idle0ToIdle1", false);
            animator.SetBool("idle0ToWalk", false);
            animator.SetBool("idle0ToRun", false);
            animator.SetBool("idle0ToWound", false);
            animator.SetBool("idle0ToSkill0", false);
            animator.SetBool("idle0ToAttack1", false);
            animator.SetBool("idle0ToAttack0", false);
            animator.SetBool("idle0ToDeath", false);
        }
        else
        {
            animator.SetBool("walkToIdle0", false);
            animator.SetBool("runToIdle0", false);
            animator.SetBool("deathToIdle0", false);
        }
    }

    private void animationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationReset();
        if (animationToPlay != "run")
        {
            Debug.Log(stateInfo.IsName("Base Layer.wound"));
        }

        if (currentState == "")
        {
            currentState = animationToPlay;
            if (currentState != "run")
            {
                Debug.Log(currentState);
            }

            if (stateInfo.IsName("Base Layer.walk") && currentState != "walk")
            {
                animator.SetBool("walkToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.death") && currentState != "death")
            {
                animator.SetBool("deathToIdle0", true);
            }

            string state = "idle0To" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            currentState = "";
        }
    }

}