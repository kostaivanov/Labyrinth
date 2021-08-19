using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
internal class PlayerJump : PlayerComponents
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float decayRate;

    private float jumpForce_2;
    Vector2 counterJumpForce = Vector2.down;


    private float extrHeightText = 0.1f;

    private bool jumpPressed;
    private bool jumpHolded;
    //public float StandartJumpForce;
    //public float MaxJumpForce;
    private float jumpForce_3;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Control control;
    private InputAction jump;

    private void Awake()
    {
        control = new Control();
    }
    
    private void OnEnable()
    {
        jump = control.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        jumpPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            jumpPressed = true;
            jumpHolded = true;
            //jumpForce_3 = MaxJumpForce;

        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpHolded = false;
            //jumpForce_3 = StandartJumpForce;

        }
        //ClampVelocityY();
        //if (!CheckIfGrounded() && !jumpPressed)
        //{
        //    StartCoroutine(ApplyCounterForce());
        //}

        //if (rigidBody.velocity.y < 0)
        //{
        //    rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if(rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
    }

    private void FixedUpdate()
    {
        if (jumpPressed == true)
        {
            //Jump();
            StartCoroutine(DoJump());

            jumpPressed = false;
        }
        if (!CheckIfGrounded())
        {
            jumpPressed = false;

        }


        if (!jumpHolded && Vector2.Dot(rigidBody.velocity, Vector2.up) > 0)
        {
            //rigidBody.AddForce(counterJumpForce * rigidBody.mass * 10f, ForceMode2D.Impulse); 
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

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    private IEnumerator DoJump()
    {
        jumpForce_2 = CalculateJumpForce(Physics2D.gravity.magnitude, jumpForce);
        //the initial jump
        Debug.Log(jumpForce_2);
        rigidBody.AddForce(Vector2.up * (jumpForce_2 * 2) * rigidBody.mass);
        yield return null;

        //can be any value, maybe this is a start ascending force, up to you
        float currentForce = jumpForce_2;

        while (Input.GetKey(KeyCode.Space) && currentForce > 0)
        {
            rigidBody.AddForce(Vector2.up * currentForce * rigidBody.mass);
            currentForce -= decayRate * Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator ApplyCounterForce()
    {
        //&& Vector2.Dot(rigidBody.velocity, Vector2.up) > 0
        while (!CheckIfGrounded())
        {
            //Debug.Log("counterforce ?");
            if (!jumpHolded && rigidBody.velocity.y > 0)
            {
                Debug.Log("counterforce ?");
                //rigidBody.velocity += Vector2.down * 20f * Time.deltaTime;
                rigidBody.AddForce(Vector2.down * rigidBody.mass * 4f, ForceMode2D.Impulse);
            }

            yield return null;
        }

        Debug.Log("does this end ?");
    }
    private void Jump()
    {
        if (CheckIfGrounded())
        {
            jumpForce_2 = CalculateJumpForce(Physics2D.gravity.magnitude, jumpForce);

            //rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            rigidBody.AddForce(Vector2.up * jumpForce_2 * rigidBody.mass, ForceMode2D.Impulse);
        }

    }
}
