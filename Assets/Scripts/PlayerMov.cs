using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    Vector2 movInput;
   
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    
    void OnMove(InputValue value)
    {
        movInput = value.Get<Vector2>();
        Debug.Log(movInput);
    }
}
