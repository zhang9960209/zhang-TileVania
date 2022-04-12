using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    [SerializeField] float MovSpeed = 1f;
    Rigidbody2D enemyRigid;
    void Start()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigid.velocity = new Vector2 (MovSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        MovSpeed = -MovSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    { 
        transform.localScale = new Vector2 (-(Mathf.Sign(enemyRigid.velocity.x)), 1f);
    }

}
