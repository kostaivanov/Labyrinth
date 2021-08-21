using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
internal class PlayerJump : PlayerComponents
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float decayRate;

    private float jumpForce_2;

    private float extrHeightText = 0.1f;

    private bool jumpPressed;
    private bool jumpHolded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Control control;
    //private InputAction movement;

    private void Awake()
    {
        control = new Control();
    }
    
    private void OnEnable()
    {
        //movement = control.Player.Movement;
        //movement.Enable();

        control.Player.Jump.performed += Jump;
        control.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        //movement.Disable();
        control.Player.Jump.Disable();
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

        //if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        //{
        //    jumpPressed = true;
        //    jumpHolded = true;
        //}
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    jumpHolded = false;

        //}

    }

    private void FixedUpdate()
    {
        if (jumpPressed == true)
        {
            StartCoroutine(JumpProcess());

            jumpPressed = false;
        }
        if (!CheckIfGrounded())
        {
            jumpPressed = false;

        }
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

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
        if (CheckIfGrounded())
        {
            StartCoroutine(JumpProcess());
        }
    }

    private IEnumerator JumpProcess()
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
}
