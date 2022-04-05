using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] float movSpeed = 10f;
    Vector2 movInput;
    Rigidbody2D playerRigid;
    Animator playerAnimator;
   
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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

    void FlipPlayer()
    {
        bool blHasHorizontalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        if (blHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigid.velocity.x), 1f);
        }
        
    }
  
}
