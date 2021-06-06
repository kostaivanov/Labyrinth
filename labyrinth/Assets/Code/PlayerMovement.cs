using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : PlayerComponents
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float extrHeightText = 0.1f;

    private bool moving;
    private bool jumpPressed;

    private float direction;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moving = false;
        jumpPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            direction = FindDirection();
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (Input.GetAxisRaw("Jump") > 0)
        {
            jumpPressed = true;
        }
    }
  
    private void FixedUpdate()
    {
        if (moving == true)
        {
            //rigidBody.MovePosition(rigidBody.position + new Vector2(direction * speed, 0) * Time.deltaTime);
            rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);

        }

        if (jumpPressed == true && CheckIfGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpPressed = false;
        }
    }

    private float FindDirection()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    internal bool CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(base.collider2D.bounds.center, base.collider2D.bounds.size, 0f, Vector2.down, extrHeightText, base.groundLayer);

        return rayCastHit.collider != null;
    }
}
