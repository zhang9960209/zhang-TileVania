using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] float movSpeed = 10f;
    Vector2 movInput;
    Rigidbody2D playerRigid;
   
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Mov();
        
        
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
    
    }
  
}
