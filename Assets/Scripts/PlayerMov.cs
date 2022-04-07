using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] float movSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 movInput;
    Rigidbody2D playerRigid;
    Animator playerAnimator;
    CapsuleCollider2D playerCapCollider;
   
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        Mov();
        FlipPlayer();
    }

    
    void OnMove(InputValue value)
    {
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
        if (!playerCapCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
  
}
