using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed=5f;
    [SerializeField] float jumpSpeed= 5f;
    [SerializeField] float climbSpeed= 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f,25f);

    bool isAlive=true;
    LayerMask ground;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet2D;
    float gravityScaleAtStart;
    void Start()
    {
        isAlive = true;
        ground = LayerMask.GetMask("Ground");
        myRigidbody =GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Run();
            ClimbLadder();
            Jump();
        }
        FlipSprite();
        Die();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocit = new Vector2(runSpeed * controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocit;
        bool playerHorizontalSpeed = Mathf.Abs(controlThrow) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && myFeet2D.IsTouchingLayers(ground))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }
    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x),1f,1f);
        }
    }
    private void ClimbLadder()
    {
        if (!myFeet2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        myRigidbody.gravityScale = 0f;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }
    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }
}
// Running Climbing