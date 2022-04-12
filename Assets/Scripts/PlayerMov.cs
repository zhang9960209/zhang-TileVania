using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] float movSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float clmbSpeed = 5f;
    Vector2 movInput;
    Rigidbody2D playerRigid;
    Animator playerAnimator;
    CapsuleCollider2D playerCapCollider;
    BoxCollider2D playerBoxCollider;
    float flGScaleStart;

    bool blIsAlive = true;
   
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapCollider = GetComponent<CapsuleCollider2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        flGScaleStart = playerRigid.gravityScale;
    }


    void Update()
    {
        if (!blIsAlive)
        {
            return;
        }
        Mov();
        FlipPlayer();
        Climb();
        Death();
    }

    
    void OnMove(InputValue value)
    {
        if (!blIsAlive)
        {
            return;
        }
        movInput = value.Get<Vector2>();
        Debug.Log(movInput);
    }

    void Mov()
    { 
        Vector2 playerVel = new Vector2(movInput.x * movSpeed, playerRigid.velocity.y);
        playerRigid.velocity = playerVel;
        bool blHasHorizontalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", blHasHorizontalSpeed);
    }

    void OnJump(InputValue value)
    {
        if (!blIsAlive)
        {
            return;
        }
        if (!playerBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        
        if (value.isPressed) 
        {
            playerRigid.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    
    
    void FlipPlayer()
    {
        bool blHasHorizontalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        if (blHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigid.velocity.x), 1f);
        }
        
    }

    void Climb()
    {
        if (!playerBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            playerRigid.gravityScale = flGScaleStart;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVel = new Vector2(playerRigid.velocity.x, movInput.y * clmbSpeed);
        bool blHasVerticalSpeed = Mathf.Abs(playerRigid.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", blHasVerticalSpeed);
        playerRigid.velocity = climbVel;
        playerRigid.gravityScale = 0f;
    }

    void Death()
    {
        if (playerCapCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            playerAnimator.SetTrigger("Death");
            blIsAlive = false;
        }
    }
  
}
