using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterContollerScript : MonoBehaviour
{
    // Moving properties
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRb;

    // Jumping properties
    public bool isGrounded;
    public float jumpForce;

    // Extended jump properties
    public float secondaryJumpForce;
    public float secondaryJumpTime;
    public bool isSecondaryJump;
    public Animator anim;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
        HandleMovement(horizontalInput);
        HandleJumping();
        UpdateCharacterDirection(horizontalInput);
        TriggerFallAnimation();
    }

    private void HandleMovement(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) > 0.1f && Mathf.Abs(myRb.velocity.x) < maxSpeed)
        {
            myRb.AddForce(new Vector2(horizontalInput * acceleration, 0), ForceMode2D.Force);
        }
    }

    private void HandleJumping()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(SecondaryJump());
        }
        if (!isGrounded && Input.GetButton("Jump") && isSecondaryJump)
        {
            myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force);
        }
    }

    private void UpdateCharacterDirection(float horizontalInput)
    {
        if (horizontalInput > 0.1f)
        {
            anim.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (horizontalInput < -0.1f)
        {
            anim.transform.localScale = new Vector3(-2, 2, 2);
        }
    }

    private void TriggerFallAnimation()
    {
        if (!isGrounded && myRb.velocity.y < 0)
        {
            anim.SetTrigger("Fall");
        }
        if (isGrounded)
        {
            anim.ResetTrigger("Fall");
            anim.SetTrigger("StopJump");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }

    IEnumerator SecondaryJump()
    {
        isSecondaryJump = true;
        anim.SetTrigger("Jumper");
        yield return new WaitForSeconds(secondaryJumpTime);
        isSecondaryJump = false;
    }
}
