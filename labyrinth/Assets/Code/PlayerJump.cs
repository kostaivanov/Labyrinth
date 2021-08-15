using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerJump : PlayerComponents
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float jumpForce_2;
    Vector2 counterJumpForce = Vector2.down;


    private float extrHeightText = 0.1f;

    private bool moving;
    private bool jumpPressed;
    private bool isJumping;
    private bool jumpHolded;
    public float StandartJumpForce;
    public float MaxJumpForce;
    private float jumpForce_3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            jumpPressed = true;
            jumpHolded = true;
            jumpForce_3 = MaxJumpForce;

        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpHolded = false;
            jumpForce_3 = StandartJumpForce;

        }

        //if (CheckIfGrounded())
        //    rigidBody.velocity.y = Mathf.Min(0, rigidBody.velocity.y) - Physics2D.gravity.magnitude * Time.deltaTime;
        //else
        //{
        //    rigidBody.velocity.y = movement.velocity.y - movement.gravity * Time.deltaTime;

        //    // When jumping up we don't apply gravity for some time when the user is holding the jump button.
        //    // This gives more control over jump height by pressing the button longer.
        //    if (jumping.jumping && jumping.holdingJumpButton)
        //    {
        //        // Calculate the duration that the extra jump force should have effect.
        //        // If we're still less than that duration after the jumping time, apply the force.
        //        if (Time.time < jumping.lastStartTime + jumping.extraHeight / CalculateJumpVerticalSpeed(jumping.baseHeight))
        //        {
        //            // Negate the gravity we just applied, except we push in jumpDir rather than jump upwards.
        //            velocity += jumping.jumpDir * movement.gravity * Time.deltaTime;
        //        }
        //    }

        //    // Make sure we don't fall any faster than maxFallSpeed. This gives our character a terminal velocity.
        //    rigidBody.velocity.y = Mathf.Max(rigidBody.velocity.y, -movement.maxFallSpeed);
        //}

        ClampVelocityY();
    }

    private void FixedUpdate()
    {
        if (jumpPressed == true)
        {
            //Jump();

            jumpPressed = false;
        }
        if (!CheckIfGrounded())
        {
            //isJumping = false;
            jumpPressed = false;

        }
    }

    public void ClampVelocityY()
    {
        float maxVelocityY = 2;
        Vector2 velocity = rigidBody.velocity;
        velocity.y = Mathf.Clamp(velocity.y, Mathf.NegativeInfinity, maxVelocityY);
        rigidBody.velocity = velocity;
        Debug.Log("dasdasdas");
    }

    internal bool CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(base.collider2D.bounds.center, base.collider2D.bounds.size, 0f, Vector2.down, extrHeightText, base.groundLayer);

        return rayCastHit.collider != null;
    }

    private void Jump()
    {
        if (CheckIfGrounded())
        {
            //isJumping = true;

            jumpForce_2 = CalculateJumpForce(Physics2D.gravity.magnitude, jumpForce);

            //rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            rigidBody.AddForce(Vector2.up * jumpForce_2 * rigidBody.mass, ForceMode2D.Impulse);
        }

    }
}
