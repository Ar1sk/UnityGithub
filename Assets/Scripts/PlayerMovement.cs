using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D cap;


    //Declare
    float dirX;
    private bool facingRight = true;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;
    private bool canMove = true;
    private bool canWallJump = true;
    private int facingDirection = 1;

    //SerializeField
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Vector2 wallJumpDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CollisionCheck();
        CheckInput();
        FlipController();
        AnimatorController();
    }

    private void FixedUpdate()
    {
        if(isGrounded)
        {
            canMove = true;
            canDoubleJump = true;
        }
        if(isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        else if(!isWallDetected)
        {
            isWallSliding = false;
            Move();
        }
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            JumpButton();

        if(canMove)
            dirX = Input.GetAxisRaw("Horizontal");
    }

    private void JumpButton()
    {
        if(isWallSliding && canWallJump)
        {
            WallJump();
        }
        else if(isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            Jump();
        }

        canWallSlide = false;
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        canMove = false;

        Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

        rb.AddForce(direction, ForceMode2D.Impulse);
    }
    private void AnimatorController()
    {
        bool isRunning = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isWallSliding", isWallSliding);
    }
    
    private void FlipController()
    {
        if(isGrounded && isWallDetected)
        {
            if (facingRight && dirX < 0)
                Flip();
            else if (!facingRight && dirX > 0)
                Flip();
        }
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Move()
    {
        if(canMove)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, groundLayer);

        if(!isGrounded && rb.velocity.y < 0)
            canWallSlide = true;
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x +wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
