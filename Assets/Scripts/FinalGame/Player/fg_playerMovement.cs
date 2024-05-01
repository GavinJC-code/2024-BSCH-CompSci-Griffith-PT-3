using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fg_playerMovement : MonoBehaviour
{
    // player movement speed
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    // player jump force
    private Rigidbody2D body;
    // player animator
    private Animator anim;
    // player box collider
    // private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    // wall jump cool down
    private float wallJumpCoolDown;
    // default gravity scale
    private float defaultGravityScale;
    // crouch
    private Vector2 originalSize;
    private Vector2 originalOffset;

    private float horizontalInput;
    public float FacingDirection { get; private set; }
    // for raycast hits
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;

    [SerializeField]private float wallJumpX;
    [SerializeField]private float wallJumpY;

    [SerializeField]private int extraJumps;
    private int jumpCounter;

    // Start is called before the first frame update
    private void Awake()
    {   // set up the variables to components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        // get the gravity scale from body.gravityScale
        defaultGravityScale = body.gravityScale;
        // Initialize original size and offset
        originalSize = capsuleCollider.size;
        originalOffset = capsuleCollider.offset;

    }
    
    // Update is called once per frame
    private void Update()
    {   
        HandleCrouching();
        // get the horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
       
        
        // flip the player left and right
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            FacingDirection = transform.localScale.x > 0 ? 1f : -1f;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            FacingDirection = transform.localScale.x > 0 ? 1f : -1f;
        }
        
        // trigger the run animation
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Check if the player is falling
        bool isFalling = !isGrounded() && body.velocity.y < 0;
        anim.SetBool("falling", isFalling);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(isGrounded())
        {
            jumpCounter = 0;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(body.velocity.y > 0)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y /2);
            }
        }
        if(onWall()){
            body.gravityScale = 0;
            body.velocity = new Vector2(0, -1);
        }
        else
        {
            body.gravityScale = defaultGravityScale;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }


    }
    //  jump function
    private void Jump()
    {   
        // condition for jumping if on the ground
        if (!onWall() && jumpCounter < extraJumps)
        {
            //debug the jumpcounter
            Debug.Log("jumpcounter" +jumpCounter);   
            //  jump force
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jumpCounter++;   
             Debug.Log("jumpcounter updated" +jumpCounter);      

        }
      
        else if(onWall() && !isGrounded())
        {
            WallJump();
        }
        
    }

    private void WallJump(){
        body.AddForce(new Vector2(wallJumpX * -transform.localScale.x, wallJumpY));
        wallJumpCoolDown = 0;
        // trigger the jump animation
       anim.SetTrigger("jump");
            
    }

    // check if the player is on the ground
    private bool isGrounded()
    {   
        // raycast to check if the player is on the ground
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f,
            Vector2.down, .1f, groundLayer);
        // return true if the player is on the ground
        return raycastHit.collider != null;
    }
    // check if the player is on the wall
    private bool onWall()
    {   
        // raycast to check if the player is on the wall
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f,
            new Vector2(transform.localScale.x,0), .1f, wallLayer);
        //  return true if the player is on the wall
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private void HandleCrouching()
{
    bool isCrouching = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    anim.SetBool("crouch", isCrouching);

    if (isCrouching)
    {
        // Reduce the collider's height by 50% and adjust the offset downward
        capsuleCollider.size = new Vector2(originalSize.x, originalSize.y / 2);
        capsuleCollider.offset = new Vector2(originalOffset.x, originalOffset.y - originalSize.y / 4.5f );
    }
    else
    {
        // Reset the collider to its original size and offset
        capsuleCollider.size = originalSize;
        capsuleCollider.offset = originalOffset;
    }
}
    
}
