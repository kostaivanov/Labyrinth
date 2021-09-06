using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal class PlayerMovement : PlayerComponents
{
    #region Constants
    private const float minimumVelocity_X = 0.5f;
    private const float minimumFallingVelocity_Y = -2f;
    private const float groundCheckRadius = 0.1f;
    #endregion

    [SerializeField] private float speed;
 
    private float extrHeightText = 0.1f;

    private bool moving;
    private float direction;

    private bool canMove;

    private Control control;
    //private InputAction movement;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    private void Awake()
    {
        control = new Control();
        control.Player.Movement.performed += context => direction = context.ReadValue<float>();
        control.Player.Movement.canceled += context => direction = 0;
    }

    private void OnEnable()
    {
        control.Player.Enable();
    }

    private void OnDisable()
    {
        control.Player.Disable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moving = false;

        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(direction);
        if (direction != 0)
        {
            moving = true;
            //direction = control.Player.Movement.ReadValue<float>();
            Debug.Log("control " + control.Player.Movement.ReadValue<float>());

        }
        //if (Input.GetAxisRaw("Horizontal") != 0 && CanMove == true)
        //{
        //    direction = FindDirection();
        //    moving = true;
        //}
        else
        {
            moving = false;
        }

    }

    private void FixedUpdate()
    {
        if (moving == true)
        {
            rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
            this.transform.localScale = new Vector2(direction * 0.65f, 0.65f);
            //rigidBody.MovePosition(rigidBody.position + new Vector2(direction * speed, 0) * Time.deltaTime);
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
        else if (moving && CheckIfGrounded())
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
