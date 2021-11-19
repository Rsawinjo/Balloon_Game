using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HW: Make Drag Effect, make player slow down so it isn't skating on ice!!!

public class DynamicPlayerController : MonoBehaviour
{
    public static DynamicPlayerController g_singleton;

    void OnEnable()
    {
        g_singleton = this;
    }

    void OnDisable()
    {
        g_singleton = null;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    public float increaseWeight;
    public float moveSpeed = 1f;
    public float jumpSpeed = 15f;
    public float maxHor = 5f;
    public float maxHorizontalMovementVelocity = 7.0f;
    public float maxDistanceFromGroundJumping = 0.2f;
    internal int goldCount = 0;    
    public void pickupGold(GoldPhysics gold)
    {
        var rigidBody = GetComponent<Rigidbody2D>();

        goldCount += gold.value;
        rigidBody.mass += gold.weight;
        goldCount += gold.value;
        //Destroy(gold.gameObject);
    }

    public int multiJumpCount = 3;
    internal int multiJumps = 0;

    public float multiJumpChargeResetTimer = 0.2f; // allot of frames.. 200 ms / 16
    internal float lastMultiJumpTime = 0;
    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Vector2 oldVelocity = rigid.velocity;


        bool wantLeft = Input.GetKey(KeyCode.LeftArrow);
        bool wantRight = Input.GetKey(KeyCode.RightArrow);
        bool isGrounded = IsGrounded();
        float timeSinceLastMultiJump = Time.time - lastMultiJumpTime;
        bool canResetJumps = timeSinceLastMultiJump > multiJumpChargeResetTimer;

        
        

        if (isGrounded && canResetJumps)
        {
            multiJumps = 0;
        }
        bool canJump = multiJumps < multiJumpCount;

        Vector2 velocityDelta = Vector2.zero;


        if (wantLeft)
        {
            velocityDelta += Vector2.left * moveSpeed;

        }

        if (wantRight)
        {
            velocityDelta += Vector2.right * moveSpeed;
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space)) 
        {
            multiJumps += 1;
            lastMultiJumpTime = Time.time;
            velocityDelta += Vector2.up * jumpSpeed;


        }

        {
            float maxIncreaseNegative = -maxHorizontalMovementVelocity - oldVelocity.x;
            float maxIncreasePositive = maxHorizontalMovementVelocity - oldVelocity.x;
            velocityDelta.x = Mathf.Clamp(velocityDelta.x, maxIncreaseNegative, maxIncreasePositive);
            rigid.velocity = oldVelocity + velocityDelta;
        }
    }
private bool IsGrounded()
{
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        float height = capsule.size.y;
        Vector2 dir = Vector2.down;
        Vector2 origin = transform.position;
        origin.y -= (height / 2.0f) * 1.01f;
        var hit = PlayerRaycast(origin, dir, maxDistanceFromGroundJumping);
        
        if (hit.collider != null)
        {
            this.transform.SetParent(hit.collider.transform, true);
            return true;
        }
        else
        {
            this.transform.SetParent(null, true);
            return false;
        }
        
        
        
}
    private RaycastHit2D PlayerRaycast(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);
        Debug.DrawLine(origin, origin + (direction * distance), Color.white);
        return hit;
    }
}

