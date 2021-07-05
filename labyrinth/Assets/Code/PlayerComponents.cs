using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class PlayerComponents : MonoBehaviour
{
    #region Unity Components
    protected Rigidbody2D rigidBody;
    protected Collider2D collider2D;
    protected Animator animator;
    //protected PlayerMovement playerMovement;
    //protected PlayerHealth playerHealth;
    protected SpriteRenderer playerSprite;
    #endregion

    internal LayerMask groundLayer;
    internal PlayerState state = PlayerState.idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        //playerMovement = GetComponent<PlayerMovement>();
        groundLayer = LayerMask.GetMask("GroundLayer");
        playerSprite = GetComponent<SpriteRenderer>();
        //playerHealth = GetComponent<PlayerHealth>();
    }
}
