using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class WallLocator : PlayerComponents
{
    [SerializeField] private float climbingHorizontalOffset;

    private Vector2 topOfPlayer;
    private GameObject ledge;
    private float animationTime = 0.5f;
    private bool falling;
    private bool moved;

    [HideInInspector]
    public bool grabbingLedge;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected virtual void FixedUpdate()
    {
        CheckForLedge();
        LedgeHanging();
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (collider2D != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(new Vector3(base.collider2D.bounds.max.x + 0.1f, base.collider2D.bounds.max.y, this.transform.position.z), Vector2.left);

        }
        //if (ledge != null && ledge.GetComponent<Collider2D>() != null)
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(new Vector3(base.collider2D.bounds.max.x + 0.1f, ledge.GetComponent<Collider2D>().bounds.center.y, this.transform.position.z), Vector2.left);
        //}

    }

    protected virtual void CheckForLedge()
    {
        if (!falling)
        {
            if (transform.localScale.x < 0)
            {
                topOfPlayer = new Vector2(base.collider2D.bounds.max.x + 0.1f, collider2D.bounds.max.y);
                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.right, 0.2f);
                if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                {
                    ledge = hit.collider.gameObject;
                    if (base.collider2D.bounds.max.y < ledge.GetComponent<Collider2D>().bounds.max.y && base.collider2D.bounds.max.y > ledge.GetComponent<Collider2D>().bounds.center.y)
                    {
                        grabbingLedge = true;
                        //base.animator.SetBool("LedgeHanging", true);
                    }
                }
            }
            else
            {
                topOfPlayer = new Vector2(base.collider2D.bounds.min.x - .1f, base.collider2D.bounds.max.y);
                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.left, .2f);
                if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                {
                    ledge = hit.collider.gameObject;
                    if (base.collider2D.bounds.max.y < ledge.GetComponent<Collider2D>().bounds.max.y && base.collider2D.bounds.max.y > ledge.GetComponent<Collider2D>().bounds.center.y)
                    {
                        //base.animator.SetBool("LedgeHanging", true);
                        grabbingLedge = true;
                    }
                }
            }
            if (ledge != null && grabbingLedge)
            {
                AdjustPlayerPosition();
                base.rigidBody.velocity = Vector2.zero;
                base.rigidBody.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                base.rigidBody.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }

    protected virtual void LedgeHanging()
    {
        if (grabbingLedge && Input.GetAxis("Vertical") > 0)
        {
            //base.animator.SetBool("LedgeHanging", false);
            if (transform.localScale.x < 0)
            {
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x + climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + base.collider2D.bounds.extents.y), animationTime - .3f));
            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x - climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + base.collider2D.bounds.extents.y), animationTime - .3f));
            }
        }
        if (grabbingLedge && Input.GetAxis("Vertical") < 0)
        {
            ledge = null;
            moved = false;
            grabbingLedge = false;
            base.animator.SetBool("LedgeHanging", false);
            falling = true;
            base.rigidBody.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<PlayerMovement>().enabled = true;
            Invoke("NotFalling", .5f);
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector2 topOfPlatform, float duration)
    {
        float time = 0;
        Vector2 startValue = transform.position;
        while (time < duration)
        {
            //base.animator.SetBool("LedgeClimbing", true);
            transform.position = Vector2.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ledge = null;
        moved = false;
        grabbingLedge = false;
        //base.animator.SetBool("LedgeClimbing", false);
    }

    protected virtual void AdjustPlayerPosition()
    {
        if (!moved)
        {
            moved = true;
            if (transform.localScale.x < 0)
            {
                transform.position = new Vector2((ledge.GetComponent<Collider2D>().bounds.min.x - base.collider2D.bounds.extents.x) + ledge.GetComponent<Ledge>().hangingHorizontalOffset, (ledge.GetComponent<Collider2D>().bounds.max.y - base.collider2D.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset);
            }
            else
            {
                transform.position = new Vector2((ledge.GetComponent<Collider2D>().bounds.max.x + base.collider2D.bounds.extents.x) - ledge.GetComponent<Ledge>().hangingHorizontalOffset, (ledge.GetComponent<Collider2D>().bounds.max.y - base.collider2D.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset);
            }
        }
    }

    protected virtual void NotFalling()
    {
        falling = false;
    }
}
