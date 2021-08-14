using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : PlayerComponents
{
    #region Constants
    private const float minimumVelocity_X = 0.5f;
    private const float minimumFallingVelocity_Y = -2f;
    private const float groundCheckRadius = 0.1f;
    #endregion

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float jumpForce_2;
    Vector2 counterJumpForce = Vector2.down;


    private float extrHeightText = 0.1f;

    private bool moving;
    private bool jumpPressed;
    private bool isJumping;

    private float direction;

    private bool canMove;

    public float fallMultiplier;
    public float lowJumpMultiplier;


    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moving = false;
        jumpPressed = false;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CheckIfGrounded() + " - bqgame weeeeeeeeeeeeeeeee = " );

        if (Input.GetAxisRaw("Horizontal") != 0 && CanMove == true)
        {
            direction = FindDirection();
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (Input.GetButtonDown("Jump") == true && !jumpPressed)
        {
            jumpPressed = true;
        }
        //else if (Input.GetKey("Jump"))
        //{
        //    jumpPressed = false;
        //}

        //else if (Input.GetButtonUp("Jump"))
        //{
        //    jumpPressed = false;
        //}

        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            Debug.Log("padaaaaaaam");
        }
        else if (rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            Debug.Log("skachaaaaam");
        }
    }
  
    private void FixedUpdate()
    {

        if (moving == true)
        {
            //rigidBody.MovePosition(rigidBody.position + new Vector2(direction * speed, 0) * Time.deltaTime);
            if (direction < 0)
            {
                rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
                this.transform.localScale = new Vector2(-0.65f, 0.65f);
            }

            if (direction > 0)
            {
                rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
                this.transform.localScale = new Vector2(0.65f, 0.65f);
            }

        }

        if (jumpPressed == true)
        {
            Jump();
            //jumpPressed = false;
        }

        if (isJumping)
        {
            if (!jumpPressed && Vector2.Dot(rigidBody.velocity, Vector2.up) > 0)
            {
                rigidBody.AddForce(counterJumpForce * rigidBody.mass * 20.0f, ForceMode2D.Impulse);
            }
        }
    }
    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    private void Jump()
    {
        if (CheckIfGrounded())
        {
            isJumping = true;

            jumpForce_2 = CalculateJumpForce(Physics2D.gravity.magnitude, jumpForce);
            Debug.Log("jumpforce = " + jumpForce_2);
            //rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            rigidBody.AddForce(Vector2.up * jumpForce_2 * rigidBody.mass, ForceMode2D.Impulse);
            //rigidBody.velocity = (Vector2.up * jumpForce);
            //jumpPressed = true;

        }
        else
        {
            jumpPressed = false;
        }
    }

    private void LateUpdate()
    {
        this.AnimationStateSwitch();
        base.animator.SetInteger("state", (int)state);
    }

    private float FindDirection()
    {
        direction = Input.GetAxisRaw("Horizontal");
        return direction;
    }

    internal bool CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(base.collider2D.bounds.center, base.collider2D.bounds.size, 0f, Vector2.down, extrHeightText, base.groundLayer);

        return rayCastHit.collider != null;
    }

    protected void AnimationStateSwitch()
    {
        
        if (rigidBody.velocity.y > 1f && CheckIfGrounded() != true)
        {
            this.state = PlayerState.jumping;
            //Debug.Log(PlayerState.jumping + " - skachame");
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") && state == PlayerState.falling && collider2D.IsTouchingLayers(groundLayer))
        {
            state = PlayerState.landing;            
        }

        else if (state == PlayerState.jumping)
        {
            if (rigidBody.velocity.y == 0 || CheckIfGrounded() == true)
            {
                state = PlayerState.idle;
                //Debug.Log(PlayerState.idle + " - idle sled skok");
            }
        }

        else if (state == PlayerState.jumping)
        {

            if (rigidBody.velocity.y < minimumFallingVelocity_Y)
            {
                state = PlayerState.falling;
                //Debug.Log(PlayerState.falling + " - padame sled skok");
            }
        }

        else if (state == PlayerState.falling)
        {
            if (collider2D.IsTouchingLayers(groundLayer))
            {
                state = PlayerState.idle;
                //Debug.Log(PlayerState.idle + " - idle sled padane");

            }
        }
        //&& Mathf.Abs(rigidBody.velocity.x) > minimumVelocity_X
        else if (moving  && CheckIfGrounded())
        {
            state = PlayerState.moving;
            //Debug.Log(PlayerState.moving + " - bqgame");

        }

        else
        {
            state = PlayerState.idle;
            //Debug.Log(PlayerState.idle + " - stoim prosto");

        }

        if (rigidBody.velocity.y < minimumFallingVelocity_Y)
        {
            state = PlayerState.falling;
            //Debug.Log(PlayerState.falling + " - padame prosto");

        }
    }
}
