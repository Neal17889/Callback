using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{


    public Animator animator;
    public Rigidbody2D rb;
    public PhysicsCheck physicscheck;
    public PlayerControllerSystem playerControllerSystem;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicscheck = GetComponent<PhysicsCheck>();
        playerControllerSystem = GetComponent<PlayerControllerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        animator.SetFloat("Velocity.X",Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Velocity.Y", rb.velocity.y);
        animator.SetBool("isGround", physicscheck.isGround);
        animator.SetBool("isDash", playerControllerSystem.isDash);
    }
}
