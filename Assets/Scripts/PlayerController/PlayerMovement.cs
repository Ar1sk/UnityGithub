using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Component
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] private Collider2D normalCap;
    private TrailRenderer trailRender;


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
    private bool isCrouching = false;
    private int facingDirection = 1;
    private Vector2 dashingDir;
    private bool canDash = true;
    private bool isDashing;

    //SerializeField
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Vector2 wallJumpDirection;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float dashingVelocity = 15f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private Transform headCheck;
    [SerializeField] private float headCheckLength;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trailRender = GetComponent<TrailRenderer>();
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
            canDash = true;
        }

        //WallSlide
        if(isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            canDoubleJump = true;
        }
        else if(!isWallDetected)
        {
            isWallSliding = false;
            Move();
        }
        //Dashing
        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            JumpButton();

        if(canMove)
        {
            dirX = Input.GetAxisRaw("Horizontal");
        }
            Dash();
    }

    private void Dash()
    {
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRender.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(0 , 0);
            }
            StartCoroutine(StopDashing());
        }

    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRender.emitting = false;
        isDashing = false;

    }

    private void Move()
    {
        if(canMove)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Crouch()
    {
        isCrouching = !isCrouching;        
    }

    private bool HeadDetect()
    {
        bool hit = Physics2D.Raycast(headCheck.position, Vector2.up, headCheckLength, groundLayer);
        return hit;
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
        anim.SetBool("isCrouching", isCrouching);
        //anim.SetBool("isCrouchWalk", isRunning);
        anim.SetBool("isDashing", isDashing);
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

    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);

        if(!isGrounded && rb.velocity.y < 0)
            canWallSlide = true;
    }

    private void OnDrawGizmos()
    {
        if (headCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x +wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Vector2 from = headCheck.position;
        Vector2 to = new Vector2(headCheck.position.x, headCheck.position.y + headCheckLength);
        Gizmos.DrawLine(from, to);
    }
}
